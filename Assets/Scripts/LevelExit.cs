using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    int currentSceneIndex;
    int nextSceneIndex;


    [SerializeField] Sprite openedDor;
    SpriteRenderer mySpriteRenderer;

    void Start(){
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col){
        
        if(col.tag == "Player"){
            mySpriteRenderer.sprite = openedDor;
            StartCoroutine(LoadNextLevel());
       }
    }

    IEnumerator LoadNextLevel(){
        yield return new WaitForSecondsRealtime(2f);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
}
