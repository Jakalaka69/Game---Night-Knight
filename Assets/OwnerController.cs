using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OwnerController : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public HealthBar healthBar;
    public GameManagerScript GM;
    // Start is called before the first frame update
    public void hit(float damage)
    {
        health += damage;
        healthBar.UpdateHealthBar(health,maxHealth);
        if(health >= maxHealth)
        {
            GM.gameOver();
        }
    }
    private void Start()
    {
        healthBar.UpdateHealthBar(health,maxHealth);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
