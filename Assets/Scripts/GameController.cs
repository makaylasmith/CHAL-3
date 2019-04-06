using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait; 
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText1;
    public Text winText2;

    private bool gameOver;
    private bool restart; 
    private int score;
    

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText1.text = "";
        winText2.text = "";
        score = 0;
        UpdateScore(); 
        StartCoroutine (SpawnWaves());
    }

    void Update()
    {

        if (Input.GetKey("escape"))
            Application.Quit();

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene("Tutorial 3");
            }
        } 
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'Y' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore(); 
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score; 
        if (score >= 100)
        {
            winText1.text = "You win!";
            winText2.text = "Game created by Makayla Smith";
            gameOver = true;
            restart = true;
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        winText2.text = "Game created by Makayla Smith";
        gameOver = true; 
    }
}   

