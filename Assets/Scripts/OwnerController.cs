using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OwnerController : MonoBehaviour
{
    public static float maxDisturbed = 10;
    public static float disturbed = 0;
    public HealthBar healthBar;
    public GameManagerScript GM;
    // Start is called before the first frame update
    public void hit(float damage)
    {
        disturbed += damage;
        healthBar.UpdateHealthBar(disturbed, maxDisturbed);
        if(disturbed >= maxDisturbed)
        {
            GM.gameOver();
        }
    }
    private void Start()
    {
        healthBar.UpdateHealthBar(disturbed, maxDisturbed);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
