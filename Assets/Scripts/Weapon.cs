using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float Damage;
    private void OnTriggerEnter(Collider other)
    { 
        
        if(other.gameObject.tag == "Enemy")
        {

            other.gameObject.GetComponent<ShirtScript>().takeDamage(Damage);
        }
       


    }
    
// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
