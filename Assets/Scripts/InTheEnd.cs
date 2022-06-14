using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTheEnd : MonoBehaviour
{
    public SpriteRenderer sprite;
    private bool CheckEnter;
    Character character;
    public Animator animator;
    public GameObject inTheEndUI;
    public GameObject interfaceUI;

    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        if (CheckEnter == true && Input.GetKeyDown(KeyCode.F) && ScoreText.Score >= 5000)
        {
            StartCoroutine("LoadScene");
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
    IEnumerator LoadScene()
    {
        animator.SetBool("fade", true);
        animator.SetBool("light", false);
        ScoreText.Score -= 5000;
        yield return new WaitForSeconds(3);
        inTheEndUI.SetActive(true);
        interfaceUI.SetActive(false);

        
    }
}
