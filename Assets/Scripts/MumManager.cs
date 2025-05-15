using System.Collections;
using System.Collections.Generic;
using DoorScript;
using UnityEngine;

public class MumManager : MonoBehaviour
{
    private float startTime;
    private float elapsedTime;
    private int count;
    [SerializeField] private Door door;
    [SerializeField] private AudioClip footsteps;
    [SerializeField] private AudioSource BGMusic;
    [SerializeField] private GameObject Mumparent;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        count = 1;
    }
    private IEnumerator FootSteps()
    {
        door.asource.clip = footsteps;
        door.asource.Play();
        print(footsteps.length);
        yield return new WaitForSeconds(5);
        
        door.OpenDoor();
        Mumparent.GetComponent<Animator>().SetBool("move", true);
        yield return new WaitForSeconds(5);
        Mumparent.GetComponent<Animator>().SetBool("move", false) ;
        yield return new WaitForSeconds(1);
        door.CloseDoor();

        
    }
    // Update is called once per frame
    void Update()
    {
        elapsedTime = Time.time - startTime;
        
        
        if((elapsedTime / 50) > count)
        {
            count++;
            StartCoroutine(FootSteps());
            
        }
    }
}
