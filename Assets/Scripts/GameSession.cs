using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int coins = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
        }

    }

    void Start(){
        livesText.text = playerLives.ToString();
        scoreText.text = coins.ToString();
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
        
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddCoin(int pointsToAdd){
        coins = coins + pointsToAdd;
        scoreText.text = coins.ToString();
    }

    void ResetGameSession(){
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }


}
