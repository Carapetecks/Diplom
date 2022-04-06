using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimator : MonoBehaviour
{
    
    public DialogManager DialogManager;
    public Dialog dialog;
    private bool CheckEnter = false;
    private void Update()
    {
        if (CheckEnter && Input.GetKeyDown(KeyCode.F)) TriggerDialog();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            CheckEnter = true;
           
            
        }
           

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckEnter = false;
          
            DialogManager.EndDialog();
        }
            
    }
    public void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }  
}
