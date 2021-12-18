using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    
    public int lifes;
    Vector3 direction;
    protected virtual void Update() 
    {
        
    }
    public void reciveDamage(int damage)
    {
        lifes -= damage;
        var rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up + transform.right + (-direction) * 10, ForceMode2D.Impulse);
        if (lifes <= 0)
        {
            base.Die();
        }
    }
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

    }
}
