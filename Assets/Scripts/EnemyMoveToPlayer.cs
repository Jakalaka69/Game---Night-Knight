using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public float speed;
    public float turnRate = 0.2f;
    // Start is called before the first frame update

    
    // Update is called once per frame
    void LateUpdate()
    {
        
        Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Player.transform.position, speed);
        Enemy.transform.LookAt(Player.transform.position);
    }
}
