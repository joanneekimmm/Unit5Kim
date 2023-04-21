using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;

    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;

    public bool isGameActive;
    public bool paused;

    private int score;
    private int lives;
    

    // Start is called before the first frame update
    void Start()
    {
         
        
;   }

    // Update is called once per frame
    void Update()
    {
        //Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        
        //when the game is active it keeps spawning
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
        
    }

   public void UpdateScore(int scoreToAdd)
    {
        //keeps track of the score
        score += scoreToAdd;
        scoreText.text = "Score:" + score; 
    }

    public void GameOver()
    {

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());        
        UpdateScore(0);
        UpdateLives(3);

        titleScreen.gameObject.SetActive(false);
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        if(lives <=0)
        {
            GameOver();
        }
    }
}
