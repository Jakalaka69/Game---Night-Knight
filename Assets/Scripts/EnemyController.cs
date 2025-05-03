using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    private GameObject player ;

    
    public float speed;
    public float turnRate = 0.2f;
    public bool dead = false;
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    public GameObject deathEffect;

    // Start is called before the first frame update

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Die()
    {

        Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
        gameObject.SetActive(false);

        if (OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            Die();

        }

    }
    // Update is called once per frame
    void LateUpdate()
    {
        
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, speed);
        gameObject.transform.LookAt(player.transform.position);
    }

    
}
