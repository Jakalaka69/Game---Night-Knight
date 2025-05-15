using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLevel2 : MonoBehaviour
{
    [SerializeField] private Transform[] chairs;
    [SerializeField] private Transform[] RCChairs;
    [SerializeField] private GameObject[] grades;
    private GameObject[] curGrades;
    private int numCollected;
    private int Grade;
    [SerializeField] private Text GradeBox;
    [SerializeField] private GameObject ReportCard;
    private string[] Grades = {"F","F","D","D","C","C","B","B","A"};
    private float timeElapsed;
    private float startTime;
    [SerializeField] private GameObject winScreen;
    public GameObject PauseUI;
    public GameObject startscreen;
    private bool isPaused;
    public AudioSource BGMusic;
    private bool isGameOver;
    public GameObject gameOverUI;
    [SerializeField] private float maxTime;
    [SerializeField] private Canvas gradeHolder;
    
    private int minutesLeft;
    private int secondsLeft;
    
    public Text timeLeftText;
    private float timeLeft;
    private int lastIndex = -1;

    // Start is called before the first frame update

    public void StartLevel()
    {
        startscreen.SetActive(false);
        Time.timeScale = 1.0f;
        BGMusic.Play();
        ChangePositions();
    }
    public void ChangePositions()
    {
        
        curGrades = GameObject.FindGameObjectsWithTag("Grade");

        foreach(GameObject go in curGrades)
        {
            Destroy(go);
        }
        
        int rand1 = Random.Range(0, RCChairs.Length);
        while(rand1 == lastIndex)
        {
            rand1 = Random.Range(0, RCChairs.Length);
        }
        lastIndex = rand1;
        ReportCard.transform.position = RCChairs[rand1].position;

        foreach (Transform t in chairs)
        {
           
            int rand = Random.Range(0, grades.Length);
            GameObject g = Instantiate(grades[rand], t.position,Quaternion.identity);
            g.transform.SetParent(gradeHolder.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {


       


        timeElapsed = Time.time - startTime;
        timeLeft = maxTime - timeElapsed;
        minutesLeft = (int)(timeLeft / 60);
        secondsLeft = (int)(timeLeft - minutesLeft * 60);


        if (secondsLeft < 10)
        {
            timeLeftText.text = minutesLeft.ToString() + ":0" + secondsLeft.ToString();
        }
        else
        {
            timeLeftText.text = minutesLeft.ToString() + ":" + secondsLeft.ToString();
        }


        if (timeElapsed >= maxTime)
        {
            gameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
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
    
    public void win()
    {
        Time.timeScale = 0.0f;
       

        BGMusic.Pause();
        if (Grades[Grade/numCollected] == "B" || Grades[Grade / numCollected] == "A"){
            winScreen.SetActive(true);
        }
        else
        {
            gameOverUI.SetActive(true);
        }
        
    }
    public static void gameOver()
    {
        Destroy(GameObject.FindGameObjectWithTag("BGMusic"));
        Time.timeScale = 0f;

        GameObject gameOverUI = GameObject.FindGameObjectWithTag("GameOverUI");
        gameOverUI.SetActive(true);
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

    private void Start()
    {
        BGMusic.Pause();
        Time.timeScale = 0.0f;
        numCollected = 0;
        Grade = 3;
        startTime = Time.time;
    }


    public void startAgain()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void mainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void UpdateGrade(int grade)
    {

        Grade += grade;
        numCollected++;
        
        GradeBox.text = "Grade: " + Grades[Grade / numCollected].ToString();
    }
}
