using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportCard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider slider;

    [SerializeField] private GameObject target;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private GameManagerLevel2 GM;
    // Update is called once per frame

    public void UpdateHealthBar(float damage)
    {

        health -= damage;
        slider.value = health / maxHealth;
        if ( health <= 0)
        {
            GM.win();
        }
        GM.ChangePositions();
        
    }
    private void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {


        transform.LookAt(target.transform.position);

    }
}
