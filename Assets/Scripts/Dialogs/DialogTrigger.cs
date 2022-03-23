using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    private void Update()
    {
        if(Input.GetButtonDown("Interaction")) TriggerDialog();
    }
    public void TriggerDialog()
    {
        
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
}
