using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    
    public int lifes;
    Vector3 direction;
    protected virtual void Update(){} 
    protected virtual void Awake(){}    
    protected virtual void Start(){}
    public void reciveDamage(int damage)
    {
        lifes -= damage;
        var rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector3.zero;
        //rigidbody.AddForce(transform.up + transform.right + (-direction) * 100, ForceMode2D.Impulse);
        rigidbody.AddForce(transform.up * 1.5f + transform.right + (-direction) * 100f, ForceMode2D.Impulse);
        if (lifes <= 0)
        {
            base.Die();
        }
    }
   
    
    
}
