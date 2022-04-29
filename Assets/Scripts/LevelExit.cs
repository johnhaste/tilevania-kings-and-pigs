using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    int currentSceneIndex;
    int nextSceneIndex;
    Animator myAnimator;
    KingMovement player;

    void Start(){
        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<KingMovement>();
    }

    void OnTriggerEnter2D(Collider2D col){
        
        if(col.tag == "Player"){
            myAnimator.SetTrigger("Opening");
            player.EnterDoor();
            StartCoroutine(LoadNextLevel());
       }
    }

    IEnumerator LoadNextLevel(){
        yield return new WaitForSecondsRealtime(1f);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
