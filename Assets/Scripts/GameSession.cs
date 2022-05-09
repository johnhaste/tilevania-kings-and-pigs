using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 5;
    [SerializeField] int coins = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image livesBar;
    [SerializeField] Sprite[] livesBarFrames;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
        }

        Scene scene = SceneManager.GetActiveScene();
    }

    void Start(){
        livesText.text = "x " + playerLives.ToString();
        scoreText.text = coins.ToString();
    }

    public void Restart(){
        SceneManager. LoadScene(SceneManager. GetActiveScene().name);
    }

    public void ProcessPlayerDeath(){
        if(playerLives > 1){
            StartCoroutine(TakeLife());
            TakeLife();
        }else{
            ResetGameSession();
        }
    }

    IEnumerator TakeLife(){
        yield return new WaitForSecondsRealtime(2f);
        playerLives--;
        
        livesText.text = "x " + playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateHearts(int playerHearts){
        //Debug.Log("Update heart:" + playerHearts);
        if(playerHearts > 0){
           playerHearts--;
        }        
        livesBar.sprite = livesBarFrames[playerHearts+1];
    }

    public void AddCoin(int pointsToAdd){
        coins = coins + pointsToAdd;
        scoreText.text = coins.ToString();
    }

    public void AddLive(){
        playerLives++;
        livesText.text = "x " + playerLives.ToString();
    }

    void ResetGameSession(){
        UpdateHearts(3);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
