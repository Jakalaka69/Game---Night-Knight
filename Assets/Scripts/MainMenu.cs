using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject[] storyBoards;
    [SerializeField] private AudioSource AS;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject mainMenu;
    private int count = 0;
    public void next()
    {
        storyBoards[count].SetActive(false);
        if(count < storyBoards.Length-1)
        {
            storyBoards[count + 1].SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
            AS.Play();
        }

        count++;
        
    }
    public void skip()
    {


        canvas.SetActive(false);
        AS.Play();

    }
    public void PlayLevel1()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Options()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        
    }
    public void Back()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);

    }
}
