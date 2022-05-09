using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Pickup : MonoBehaviour
{

    [SerializeField] AudioClip currentPickupSFX;
    [SerializeField] int pointsForPickup = 100;

    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D col){
        
        if(col.tag == "Player" && !wasCollected){
            if(gameObject.tag == "Coin"){
                wasCollected = true;
                FindObjectOfType<GameSession>().AddCoin(pointsForPickup);
                AudioSource.PlayClipAtPoint(currentPickupSFX, Camera.main.transform.position);
                Destroy(gameObject);
            }else if(gameObject.tag == "ExtraLive"){
                wasCollected = true;
                FindObjectOfType<GameSession>().AddLive();
                AudioSource.PlayClipAtPoint(currentPickupSFX, Camera.main.transform.position);
                Destroy(gameObject);
            }
           
       }
    }
}
