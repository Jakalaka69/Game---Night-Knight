using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class AnimationToRagdoll : MonoBehaviour
{

    [SerializeField] Player player;
    [SerializeField] Collider myCollider;
    [SerializeField] AnyStateAnimator myStateAnimator;
  
    [SerializeField] float respawnTime = 30F;
    Rigidbody[] rigidbodies;
    bool bIsRagdoll = false;
    [SerializeField]
    public bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

   

    private void ToggleRagdoll(bool bisAnimating)
    {
        bIsRagdoll = !bisAnimating;
        myCollider.enabled = bisAnimating;
        foreach(Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = bisAnimating;
        }
        
        myStateAnimator.animator.enabled = bisAnimating;
        
        if (bisAnimating)
        {
            
            myStateAnimator.TryPlayAnimation("Stand");
        }
    }


    private IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(respawnTime);
        ToggleRagdoll(true);
        myStateAnimator.OnAllAnimationsDone();
        dead = false;
        player.SetDead(false);
    }

    void die()
    {
        ToggleRagdoll(false);
        StartCoroutine(GetBackUp());
        
    }

    
    // Update is called once per frame
    private void Update()
    {
        
        if (!bIsRagdoll && dead == true)
        {
            die();
        }
    }
}
