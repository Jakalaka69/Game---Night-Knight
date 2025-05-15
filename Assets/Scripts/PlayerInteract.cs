using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform cuddleSpot;
    [SerializeField] private GameObject toolTip;
    [SerializeField] AnyStateAnimator animator ;
    [SerializeField] CharacterController controller;
    [SerializeField] PlayerController playerController;
    private PlayerActions actions;
    private bool cuddling;
    void Start()
    {
        actions = playerController.actions;
        cuddling = false;
    }


    private void Cuddle()
    {
        cuddling = true;
        animator.TryPlayAnimation("Cuddle");
        actions.Disable();
        controller.enabled = false;
    }
    private void StopCuddle() { 
        cuddling = false;
        animator.OnAnimationDone("Cuddle");
        actions.Enable();
        controller.enabled = true;
    }
        

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(cuddleSpot.position,transform.position);
        if(dist < 2)
        {
            toolTip.SetActive(true);
            if (!cuddling)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {

                    Cuddle();
                }
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    StopCuddle();
                }
            }
            
        }
        else
        {
            toolTip.SetActive(false);
        }
;   }
}
