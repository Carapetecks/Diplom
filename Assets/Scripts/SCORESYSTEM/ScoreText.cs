using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public static int Score;
    Text scoreText;
    public DialogueManager dialogueManager;
    public DialogueTrigger dialogueTrigger;
    private void Start()
    {
        scoreText = GetComponent<Text>();
    }
    private void Update()
    {
        scoreText.text = Score.ToString();
        if(Score >= 500)
        {
            ChangeFileName(name);
        }
    }
    void ChangeFileName(string name)
    {        
        name = "dialogue3";
        dialogueTrigger.fileName = name;
    }    

}
