using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    
    public int lifes;
    Vector3 direction;
    protected virtual void Update(){} 
    protected virtual void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }    
    protected virtual void Start(){}
    public override void reciveDamage(int damage)
    {
        base.reciveDamage(damage);
        if (lifes <= 0)
        {
            base.Die();
        }
    }

    public Monster() : base()
    {

    }




}
