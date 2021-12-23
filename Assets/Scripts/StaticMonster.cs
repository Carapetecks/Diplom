﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMonster : Monster
{
    private Character mainCharacter;
    private Bullet bullet;
    Vector3 direction;
    public bool faceRight = true;
    private SpriteRenderer sprite;

    protected override void Start()
    {
        direction = transform.right;
        InvokeRepeating("Shoot", 0, 5);
    }

    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void Shoot() // стрельба
    {
        Vector3 position = transform.position;
        position.y += 0.35f;       
        position.x -= 0.1f;       
        Bullet newBullet = Instantiate
          (bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? 1.0f : -1.0f);

    }
 

}