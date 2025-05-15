using System.Collections;
using System.Collections.Generic;
using DoorScript;
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
    [SerializeField] private GameObject player;
    [SerializeField] private Door door;
    [SerializeField] private Lamp lamp;
    public bool spawners;
    private GameObject Owner;
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
        
        SoundEffectManager.Instance.PlaySoundFXClip(gameOverSound, player.transform, 1f);
        gameOverUI.SetActive(true);
    }
    public void Restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    
    private void Start()
    {

        spawners = true;
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


        if (timeElapsed >= maxTime)
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


        if(door.open == true)
        {
            spawners = false;
            lamp.LampOff();
            BGMusic.Pause();
            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemies.Length > 0 )
            {
                print(enemies.Length);
                foreach (GameObject enemy in enemies)
                {
                    print("here");
                    enemy.GetComponent<ShirtScript>().SilentDie();

                }
            }
            enemies = GameObject.FindGameObjectsWithTag("Water");
            if (enemies.Length > 0)
            {
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<WaterScript>().Die();

                }
            }
            


            StartCoroutine(CheckCuddle());
        }
        else
        {
           
            if(BGMusic.isPlaying == false && Time.timeScale == 1.0f){
                
                BGMusic.Play();
            }

            StartCoroutine(SpawnersOn());
        }

    }
    public IEnumerator CheckCuddle()
    {
        yield return new WaitForSeconds(0.5f);
        if (player.GetComponent<PlayerController>().cuddling == false)
        {
            gameOver();
        }
    }
    public IEnumerator DeathEffect(GameObject effect, Transform transform)
    {
        GameObject Effect = Instantiate(effect, transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(1);
        Destroy(Effect);
    }
    public IEnumerator SpawnersOn()
    {
        yield return new WaitForSeconds(1);
        spawners = true;
    }

    public void Resume()
    {
        BGMusic.UnPause();
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

    public void ToShop()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
