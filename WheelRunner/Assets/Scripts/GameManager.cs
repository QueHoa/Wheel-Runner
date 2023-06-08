using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStart;
    public GameObject startText;
    public static int numberOfCoin;
    public Text coinText;
    public AudioClip gameMusicClip;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        gameOver = false;
        Time.timeScale = 1;
        isGameStart = false;
        numberOfCoin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        coinText.text = "Coins: " + numberOfCoin.ToString();
        /*if (Input.GetKey(KeyCode.Space))
        {
            isGameStart = true;
            Destroy(startText);
        }*/
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        isGameStart = true;
        Destroy(startText);
    }
}
