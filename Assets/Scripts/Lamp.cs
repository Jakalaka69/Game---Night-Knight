using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Lamp : MonoBehaviour
{
    public Material off;
    public Material on;
    public MeshRenderer MR;
    public GameObject lightSource;
    public bool disturb;
    public OwnerController OC;
    float count = 0;
    float grace;
    private float timeSinceLast;
    public int maxSpawnRate;
    public float lastTime;
    private float nextTime;

    public void LampOn()
    {
        lightSource.SetActive(true);
        MR.material = on;
        
        
    }
    public void LampOff()
    {
        lightSource.SetActive(false);
        MR.material = off;
        disturb = false;
    }


    // Start is called before the first frame update
    void Awake()
    {
        nextTime = Random.Range(6, maxSpawnRate);
    }
    void Disturb()
    {
        OC.hit(0.25f);
        
    }
    // Update is called once per frame
    void Update()
    {
        

        timeSinceLast = Time.time - lastTime;
        if ((timeSinceLast > nextTime) && Time.timeScale != 0f && !disturb)
        {
            lastTime = Time.time;
            nextTime = Random.Range(6, maxSpawnRate);
            LampOn();
            disturb = true;
            count = Time.time;
            grace = Time.time;
        }
        else if (disturb)
        {
            
            if(Time.time - count > 1 && Time.time - grace > 3)
            {
                Disturb();
                count = Time.time;
                grace = 0;
            }
            
        }
    }
}
