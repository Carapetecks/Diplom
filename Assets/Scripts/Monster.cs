﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{   
    protected virtual void Update(){} 
    protected virtual void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }    
    protected virtual void Start(){}
    public override void reciveDamage(int damage)
    {
        base.reciveDamage(damage);
    }
    public Monster() : base()
    {

    }
}
