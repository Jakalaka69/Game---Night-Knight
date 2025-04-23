using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform lookAtTarget;
    [SerializeField]
    private Transform followTarget;

    private void LateUpdate()
    {
        transform.LookAt(lookAtTarget,Vector3.up);
        Rotate();
    }
    private void Rotate()
    {
        transform.position = followTarget.position;
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
