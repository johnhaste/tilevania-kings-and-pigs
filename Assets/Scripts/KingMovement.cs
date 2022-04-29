using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class KingMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(20f,20f);
    [SerializeField] GameObject hammerAttack;
    [SerializeField] Transform hammer;

    float playerGravity;
    [SerializeField] public int playerHearts = 3;
    bool isAlive;
    bool isEnteringDoor = false;
    public bool isFlashing;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myFeetCollider;
    [SerializeField] AudioClip hammerAttackSFX;
    [SerializeField] AudioClip openDoorSFX;

    void Start()
    {
        isAlive = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        playerGravity = myRigidBody.gravityScale;
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider  = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GameObject.FindGameObjectWithTag("PlayerFeet").GetComponent<BoxCollider2D>();
       
    }

    public void EnterDoor(){
        myRigidBody.velocity = new Vector2(0f, 0f);
        isEnteringDoor = true;
        AudioSource.PlayClipAtPoint(openDoorSFX, Camera.main.transform.position);
        myAnimator.SetTrigger("EnteringDoor");
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<GameSession>().UpdateHearts(playerHearts);
        if(!isAlive || isEnteringDoor){return;}
        //Always updates the player horizontal velocity
        Run();
        ClimbLadder();
        FlipSprite();
        Die();
    }

    void ClimbLadder()
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            //When the player is climbing it shouldn't fall
            myRigidBody.gravityScale = 0;
            //Updates velocity according to the input
            Vector2 climbVelocity = new Vector2 (myRigidBody.velocity.x, moveInput.y * climbSpeed);
            myRigidBody.velocity = climbVelocity;

            //Checks if the player is moving vertically
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;

            if(playerHasVerticalSpeed){
                //Updates animation
                myAnimator.SetBool("isClimbing", true);
            }else{
                //Updates animation
                myAnimator.SetBool("isClimbing", false);
            }
            
        }else{
             //Updates animation
            myAnimator.SetBool("isClimbing", false);
            //When the player is NOT climbing it  fall
            myRigidBody.gravityScale = playerGravity;
        }
        
    }

    void OnMove(InputValue value){
        //It's triggered by the arrows
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        //Updates velocity according to the input
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        //Updates animator (Could also be myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);)
        myAnimator.SetBool("isRunning", true);
    }

    void OnJump(InputValue value){

        //If not touching the groud
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){

        }else{
            if(value.isPressed){
                myRigidBody.velocity += new Vector2(0f, jumpSpeed);
            }
        }
    }

    void FlipSprite(){

        //Checks if player is moving horizontally
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        
        //If it's running
        if(playerHasHorizontalSpeed){
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }else{
            myAnimator.SetBool("isRunning", false);
        }
    }

    void OnFire(InputValue value){
        if(!isAlive){return;}
        AudioSource.PlayClipAtPoint(hammerAttackSFX, Camera.main.transform.position);
        myAnimator.SetBool("isAttacking", true);
        StartCoroutine(FinishAttack());
        Instantiate(hammerAttack, hammer.position, transform.rotation);
    }

    IEnumerator FinishAttack(){
        yield return new WaitForSecondsRealtime(0.2f);
        myAnimator.SetBool("isAttacking", false);
    }

    void Die(){
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")) || playerHearts <= 0)
        {
            playerHearts = -1;
            FindObjectOfType<GameSession>().UpdateHearts(playerHearts);
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            ThrowPlayer();
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

    }
    public void ThrowPlayer(){
        if(playerHearts > 0)
            myRigidBody.velocity = deathKick;
    }

    public IEnumerator Flash(){
        isFlashing = true;
        //How long it flashes
        float flashDuration = 0.2f;

        //Get the SpriteRenderer Component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        //Start flashing
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.enabled = true;

        isFlashing = false;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "FallingPlatform" && gameObject.transform.position.y > col.transform.position.y){
            Wait(2f);
            col.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public IEnumerator Wait(float seconds){
        yield return new WaitForSeconds(seconds);
    }
}
