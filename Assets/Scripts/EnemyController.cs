using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UIElements;
using static GameManagerScript;

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
    [SerializeField] AudioClip damageClip;
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
        
        if (health <= 0) {
            Die();
        }
    }

    public void Die()
    {
        SoundEffectManager.Instance.PlaySoundFXClip(damageClip, transform, 1f);
        Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    public void SilentDie()
    {

        Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        

    }
    private void OnTriggerEnter(Collider collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "OwnerHead")
        {
            collision.gameObject.GetComponent<OwnerController>().hit(contactDamage);
            Die();

        }
        
        

    }
    // Update is called once per frame
    void LateUpdate()
    {
        
        if(Time.timeScale != 0f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Owner.transform.position, speed);
            gameObject.transform.LookAt(Owner.transform.position);
        }
        
    }

    
}
