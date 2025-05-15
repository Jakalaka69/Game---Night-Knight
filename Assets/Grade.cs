using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grade : MonoBehaviour
{
    [SerializeField] private int grade;
    [SerializeField] private GameObject ownerLevel2;
    [SerializeField] private GameObject target;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        print("here");
        ownerLevel2.GetComponent<GameManagerLevel2>().UpdateGrade(grade);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Start()
    {
        ownerLevel2 = GameObject.FindGameObjectWithTag("GameManager");
        target = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void Update()
    {
        transform.LookAt(target.transform.position);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
