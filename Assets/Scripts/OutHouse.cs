using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutHouse : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    private bool CheckEnter = false;
    Character character;
    public float fadeTime = 5;
    public float[] position;
    private int lifes;

    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (CheckEnter == true && Input.GetKeyDown(KeyCode.F))
        {
            ToHouse();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            CheckEnter = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            CheckEnter = false;
    }

    IEnumerator LoadScene(Character character)
    {
        animator.SetBool("fade", true);
        animator.SetBool("light", false);
        yield return new WaitForSeconds(3);
        
    }


    public void ToHouse()
    {
        StartCoroutine(LoadScene(character.GetComponent<Character>()));

    }
}
