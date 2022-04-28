using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementPig : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    [SerializeField] Vector2 deathKick = new Vector2(20f,20f);
    public bool isAlive;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        isAlive = true;
    }

    void Update()
    {
        if(isAlive){
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }else{
            myRigidBody.velocity = new Vector2(0f, 0f);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag != "Player" && other.tag != "PlayerFeet" && other.tag != "PlayerBody"){
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }        
    }

    public void Dies(){
        isAlive = false;
        myAnimator.SetTrigger("Dying");
        StartCoroutine(Flash());
    }

    public IEnumerator Flash(){
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
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.enabled = true;
        

        Destroy(gameObject);
    }

    void FlipEnemyFacing(){
        //Flip Horizontally
        transform.localScale = new Vector2((Mathf.Sign(myRigidBody.velocity.x)), gameObject.transform.localScale.y);
    }
}
