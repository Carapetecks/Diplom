using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FallenTrap : Monster
{
    Rigidbody2D rb;
    
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Character)
        {
            rb.isKinematic = false;
            
        }
        if(rb.isKinematic == false)
        {
            Destroy(gameObject, 1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.name.Equals("Character"))
        {
            Destroy(gameObject);
        }
        //if (collision.gameObject.name.Equals("Character"))
        //{          
        //    Destroy(gameObject, 0.2f);
        //}
        //collision.gameObject.name.Equals("Character")
    }
    void Update()
    {
        
    }
}
