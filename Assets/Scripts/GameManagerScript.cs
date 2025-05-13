using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject PauseUI;
    private bool isPaused;
    public AudioSource BGMusic;
    private bool isGameOver;
    public GameObject gameOverUI;
    [SerializeField] private AudioClip gameOverSound;
    private float startTime;
    public Text timeLeftText;
    private float timeLeft;
    [SerializeField] private float maxTime;
    private float timeElapsed;
    private int minutesLeft;
    private int secondsLeft;
    [SerializeField] private GameObject[] tips;
    private int tip;
    [SerializeField] private GameObject winScreen;
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
        
    }

    public void nextTip()
    {
        tips[tip].SetActive(false);
        if(tip == tips.Length - 1)
        {
            tips[0].SetActive(true);
            tip = 0;
        }
        else
        {
            tips[tip+1].SetActive(true);
            tip++;
        }
    }
    public void gameOver()
    {
        Destroy(BGMusic);
        Time.timeScale = 0f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SoundEffectManager.Instance.PlaySoundFXClip(gameOverSound, player.transform, 1f);
        gameOverUI.SetActive(true);
    }
    public void Restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    private void Awake()
    {
        Time.timeScale = 0.0f;
        startTime = Time.time;
        tip = 0;
    }
    public void StartLevel()
    {

        tips[tip].SetActive(false);
        Time.timeScale = 1.0f;
        BGMusic.Play();
    }
    public void win()
    {
        Time.timeScale = 0.0f;
        winScreen.SetActive(true);
        BGMusic.Pause();
    }
    
    void Update()
    {
        timeElapsed = Time.time - startTime;
        timeLeft = maxTime - timeElapsed;
        minutesLeft = (int)(timeLeft / 60);
        secondsLeft = (int)(timeLeft - minutesLeft*60);
        if(secondsLeft < 10)
        {
            timeLeftText.text = minutesLeft.ToString() + ":0" + secondsLeft.ToString();
        }
        else
        {
            timeLeftText.text = minutesLeft.ToString() + ":" + secondsLeft.ToString();
        }
        

        if(timeElapsed >= maxTime)
        {
            win();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameOver)
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            
        }
    }

    public void Resume()
    {
        BGMusic.Play();
        PauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    void Pause()
    {
        BGMusic.Pause();
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
