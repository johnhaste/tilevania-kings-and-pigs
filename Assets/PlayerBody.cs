using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] KingMovement player;

    void OnTriggerEnter2D(Collider2D col){
        
        if(col.tag == "Enemy"){
            if(!player.isFlashing){
                //Debug.Log("Leva Dano");
                player.ThrowPlayer();  
                player.playerHearts--; 
                FindObjectOfType<GameSession>().UpdateHearts(player.playerHearts);  
                StartCoroutine(player.Flash()); 
            }
        } 
    }
}
