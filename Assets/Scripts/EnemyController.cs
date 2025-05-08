using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UIElements;

public class ShirtScript : MonoBehaviour
{
    private GameObject player ;
    private GameObject Owner;

    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] HealthBar healthBar;
    public float speed;
    public float turnRate = 0.2f;
    public bool dead = false;
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    public GameObject deathEffect;
    public float contactDamage;

    // Start is called before the first frame update

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Owner = GameObject.FindGameObjectWithTag("Owner");
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);

    }

    public void takeDamage(float amount)
    {
        health -= amount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if(health <= 0) {
            Die();
        }
    }

    public void Die()
    {

        Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
        gameObject.SetActive(false);

        
    }
    private void OnTriggerEnter(Collider collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "OwnerHead")
        {
            
            Die();

        }
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().takeDamage(contactDamage);
            Die();
        }
        

    }
    // Update is called once per frame
    void LateUpdate()
    {
        

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Owner.transform.position, speed);
        gameObject.transform.LookAt(Owner.transform.position);
    }

    
}
