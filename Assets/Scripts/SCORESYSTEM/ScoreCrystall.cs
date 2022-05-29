using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCrystall : MonoBehaviour
{
    private bool CheckEnter = false;

    private void Update()
    {
        TakeCrystall();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            CheckEnter = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            CheckEnter = false;
    }

    public void TakeCrystall()
    {
        if(CheckEnter==true && Input.GetKeyDown(KeyCode.F))
        {
            ScoreText.Score += 500;
            Debug.Log(ScoreText.Score);
            Destroy(gameObject);
        }
    }
}
