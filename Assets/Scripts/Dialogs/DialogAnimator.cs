using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimator : MonoBehaviour
{
    public Animator fAnimation;
    public DialogManager DialogManager;
    public void OnTriggerEnter2D(Collider2D other)
    {
        fAnimation.SetBool("fOpen", true);
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        fAnimation.SetBool("fOpen", false);
        DialogManager.EndDialog();
    }
}
