using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementNew : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other){
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing(){
        //Flip Horizontally
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}
