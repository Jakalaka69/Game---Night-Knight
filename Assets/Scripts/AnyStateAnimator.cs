using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnyStateAnimator : MonoBehaviour
{
    private Animator animator;

    private Dictionary<string,AnyStateAnimation> anyStateAnimations = new Dictionary<string,AnyStateAnimation>();

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
            foreach(string animName in anyStateAnimations.Keys.ToList())
            {
                anyStateAnimations[animName].IsPlaying = false;
            }
            anyStateAnimations[animationName].IsPlaying = true;
        }
    }
    
}
