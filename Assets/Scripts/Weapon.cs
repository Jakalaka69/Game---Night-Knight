using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float Damage;
    
    private void OnTriggerEnter(Collider other)
    { 
        
        if(other.gameObject.tag == "Enemy" && GetComponentInParent<PlayerController>().isAttacking)
        {
            GetComponentInParent<PlayerController>().isAttacking = false;
            GetComponentInParent<PlayerController>().Balance  +=  5;
            other.gameObject.GetComponent<ShirtScript>().takeDamage(Damage);

        }
        if (other.gameObject.tag == "Lamp" && GetComponentInParent<PlayerController>().isAttacking)
        {
            GetComponentInParent<PlayerController>().isAttacking = false;
            other.gameObject.GetComponent<Lamp>().LampOff();
        }



    }
    

}
