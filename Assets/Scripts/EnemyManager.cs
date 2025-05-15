using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    
    public int maxSpawnRate;
    private float timeSinceLast;
    private float lastTime;
    private float nextTime;
    [SerializeField] private GameManagerScript gameManagerScript;
    // Start is called before the first frame update

    void Awake()
    {
        nextTime = Random.Range(3,maxSpawnRate);
    }
    private void Update()
    {
        if(gameManagerScript.spawners == true)
        {
            timeSinceLast = Time.time - lastTime;
            if ((timeSinceLast > nextTime) && Time.timeScale != 0f)
            {
                lastTime = Time.time;
                nextTime = Random.Range(3, maxSpawnRate);
                spawnNewEnemy();
            }
        }
        
    }


    void spawnNewEnemy()
    {
        int rand = Random.Range(0,spawnPoints.Length);
        int rand2 = Random.Range(0,enemyPrefabs.Length);
        Instantiate(enemyPrefabs[rand2], spawnPoints[rand].transform.position, Quaternion.identity);
        
    }

}
