using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    [SerializeField] private Text Balance;
    private float balance;
    [SerializeField] private Text HealthCur;
    private float healthCur;
    [SerializeField] private Button HealthPlus;
    [SerializeField] private GameObject Intro;
    [SerializeField] private Button HealthMinus;
    [SerializeField] private Text HealthPrice;
    private float healthPrice = 5;
    [SerializeField] private Text MSCur;
    private int msCur = 10;
    [SerializeField] private Button MSPlus;
    [SerializeField] private Button MSMinus;
    [SerializeField] private Text MSPrice;
    private float msPrice = 5;
    public static float startBalance;
    public static float startHealth;
    public static float startSpeed = 10;
    

    public void LoadLevel2()
    {

        PlayerController.Balance = balance;
        PlayerController.maxHealth = healthCur;
        PlayerController.walkSpeed *= msCur / startSpeed;
        PlayerController.runSpeed *= PlayerController.walkSpeed = msCur / startSpeed;
        
        SceneManager.LoadSceneAsync(3);
    }
    private void Start()
    {
        
        startHealth = PlayerController.maxHealth;
        startBalance = PlayerController.Balance;
        balance = startBalance;
        healthCur = startHealth;
        
        float discount = 1;
        if( OwnerController.disturbed < OwnerController.maxDisturbed / 2)
        {
            discount = 0.8f;
        }
        msPrice = 5 * discount;
        healthPrice = 5 * discount;
        MSPrice.text = msPrice.ToString();   
        HealthPrice.text = healthPrice.ToString();
        Balance.text = balance.ToString();
    }
  

    public void MSPlusOnClick()
    {
        print(balance + " " + msPrice);
        if(balance >= msPrice)
        {
            balance -= msPrice;
            Balance.text = balance.ToString();
            msCur += 1;
            MSCur.text = "Current: " + msCur.ToString();
        }
        
    }
    public void MSMinusOnClick()
    {
        if (msCur > startSpeed)
        {
            balance += msPrice;
            Balance.text = balance.ToString();
            msCur -= 1;
            MSCur.text = "Current: " + msCur.ToString();
        }
        
    }

    public void HealthMinusOnClick()
    {
        if (healthCur > startHealth)
        {
            balance += healthPrice;
            Balance.text = balance.ToString();
            healthCur -= 1;
            HealthCur.text = "Current: " + healthCur.ToString();
        }
        
    }
   public void HealthPlusOnClick()
    {

        if (balance >= healthPrice)
        {
            balance -= healthPrice;
            Balance.text = balance.ToString();
            healthCur += 1;
            HealthCur.text = "Current: " + healthCur.ToString();
        }

    }

    public void RemoveIntro()
    {
        Intro.SetActive(false);
    }

}
