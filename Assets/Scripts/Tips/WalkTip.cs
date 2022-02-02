using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTip : MonoBehaviour
{
    public bool enterCheck;
    public GameObject tip;
    
    private void Start()
    {
        enterCheck = false;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && enterCheck == false)
        {
            enterCheck = true;
            tip.SetActive(true);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && enterCheck == true)
        {
            enterCheck = false;
            tip.SetActive(false);
        }
        
    }
}
