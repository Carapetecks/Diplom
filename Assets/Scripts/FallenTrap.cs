using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenTrap : MonoBehaviour
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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.name.Equals("Character"))
        {
             //unit.reciveDamage();
             Destroy(gameObject, 0.2f);

        }
        //collision.gameObject.name.Equals("Character")

    }
    void Update()
    {
        
    }
}
