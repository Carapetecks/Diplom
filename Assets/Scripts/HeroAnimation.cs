using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimation : MonoBehaviour
{
    public Animator animator;
    public Character character;

    private void Start()
    {
        animator = GetComponent<Animator>();
       // character = GetComponent<Character>();
    }

    public void AnimationPlay()
    {
        if(animator)
        {
            animator.SetBool("Run", Mathf.Abs(character.speed) >= 0.1f);
        }
    }

}
