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
    public Text scoreText;
    public AudioClip gameMusicClip;
    public AudioClip gameOverClip;
    //[HideInInspector]
    public static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        gameOver = false;
        Time.timeScale = 1;
        isGameStart = false;
        numberOfCoin = 0;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            audioSource.clip = gameOverClip;
            audioSource.loop = false;
            scoreText.text = "Coins: " + numberOfCoin.ToString();
            gameOverPanel.SetActive(true);
            coinText.gameObject.SetActive(false);
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
        coinText.gameObject.SetActive(true);
    }
}
