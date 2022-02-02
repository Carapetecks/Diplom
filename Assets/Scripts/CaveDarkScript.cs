using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveDarkScript : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    public float fadeTime = 5;
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        
        if (collision != null && unit && unit is Character)
        {
            StartCoroutine(OpenMenu());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (collision != null && unit && unit is Character)
        {
            animator.SetBool("light", true);
            animator.SetBool("fade", false);
        }
    }
    IEnumerator OpenMenu()
    {
        animator.SetBool("fade", true);
        animator.SetBool("light", false);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Menu");
    }
}
