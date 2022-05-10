using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScoreCalculator : MonoBehaviour
{


     void OnTriggerEnter2D(Collider2D col){
        
        if(col.tag == "Player"){
            FindObjectOfType<GameSession>().UpdateFinalScore();
        }           
    }
}

