using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnyStateAnimator : MonoBehaviour
{
    public Animator animator;

    public Dictionary<string,AnyStateAnimation> anyStateAnimations = new Dictionary<string,AnyStateAnimation>();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       Animate();
    }

    public bool GetXState(string state)
    {
        return animator.GetBool(state);
    }

    public void TryPlayAnimation(string animationName)
    {
        
        bool startAnimation = true;
        if (anyStateAnimations[animationName].HigherPrio == null)
        {
            StartAnimation();
        }
        else
        {
            foreach(string animName in anyStateAnimations[animationName].HigherPrio)
            {
                if (anyStateAnimations[animName].IsPlaying == true)
                {
                    startAnimation = false;
                    break;
                }
            }
            if (startAnimation)
            {
                StartAnimation();
            }
        }
        void StartAnimation()
        {
              foreach (string animName in anyStateAnimations.Keys.ToList())
                {
                    if(animName != "Fight")
                    {
                    anyStateAnimations[animName].IsPlaying = false;

                    }
                   
                }
            
            
            anyStateAnimations[animationName].IsPlaying = true;
        }
        
    }
    public void AddAnimation(params AnyStateAnimation[] animations)
    {
        for(int i = 0; i < animations.Length; i++)
        {
            this.anyStateAnimations.Add(animations[i].AnimationName, animations[i]);
        }
    }

    public void Animate()
    {
        foreach (string key in anyStateAnimations.Keys)
        {
            animator.SetBool(key, anyStateAnimations[key].IsPlaying);
        }
    }
    public void OnAllAnimationsDone()
    {
        foreach(string key in anyStateAnimations.Keys)
        {
            
            animator.SetBool(key, false);
            
            OnAnimationDone(key);
        }
    }
    public void OnAnimationDone(string animationName)
    {
        if(animationName == "Fight")
        {
            GetComponent<PlayerController>().isAttacking = false;
        }
        
        anyStateAnimations[animationName].IsPlaying = false;
    }
}
