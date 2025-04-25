using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    public Transform spawnPoint;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    

    private void OnEnable()
    {
        EnemyScript.OnEnemyKilled += spawnNewEnemy;
    }


    void spawnNewEnemy()
    {

        Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
        
    }

}
