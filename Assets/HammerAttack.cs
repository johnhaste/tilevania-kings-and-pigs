using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAttack : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    KingMovement player;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<KingMovement>();
        //transform.localScale = new Vector2(player.transform.localScale.x, transform.localScale.y);
        Debug.Log(player.transform.localScale.x);
        transform.localScale = new Vector2(player.transform.localScale.x, 1f);
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear(){
        yield return new WaitForSecondsRealtime(0.1f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Enemy"){
            Destroy(col.gameObject);
        }
    }
}
