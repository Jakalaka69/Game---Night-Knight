using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UIElements;


public class WaterScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    public float contactDamage = 5;
    public float speed;
    public float turnRate = 0.2f;
    public bool dead = false;
    public Vector3 target;
 
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    // Start is called before the first frame update

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform.position;
        rb = GetComponent<Rigidbody>();
        
        rb.velocity = (target - gameObject.transform.position + new Vector3(0,1,0)) / 2;
    }
    public void Die()
    {
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
            collision.gameObject.GetComponent<PlayerController>().takeDamage(contactDamage);
            
        }
        Die();

    }
    // Update is called once per frame
    void LateUpdate()
    {

        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, speed);
        
    }


}
