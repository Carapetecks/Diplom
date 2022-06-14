using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster
{
    public float speed;
    public float agroDistance;
    public float mobAttackRange;
    public float timeToAttack;
    public float distanceToCharacter;
    private float currentTimeToAttack;
    public float minSeed;
    public float maxSeed;

    private Animator animator;
    public Transform character;
    public Transform mobAttackPoint;
    public Transform mobAgroPoint;
    public LayerMask characterLayer;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, character.position);
        if(distToPlayer< agroDistance)
        {
            animator.SetBool("Fly", true);
            Hunting();
        }
        distanceToCharacter = distToPlayer;
        if(lifes<=0)
        {
            base.Die();
        }

        if(distToPlayer <= 0.2)
        {
            speed = minSeed;
        }
        else
        {
            speed = maxSeed;
        }
    }
    private void FixedUpdate()
    {
        if (currentTimeToAttack > 0)
        {
            currentTimeToAttack -= Time.deltaTime;
        }

        if ( currentTimeToAttack <= 0)
        {
            MonsterAttack();
            currentTimeToAttack = timeToAttack;
        }
    }

    private void Hunting()
    {
        if(character.position.x < transform.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * Time.deltaTime);
            transform.localScale = new Vector3(1, 1, 1);
        }else if(character.position.x > transform.position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * Time.deltaTime);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void MonsterAttack()
    {
        Collider2D[] units = Physics2D.OverlapCircleAll(mobAttackPoint.position, mobAttackRange, characterLayer);
        if (units.Length > 0)
        {
            for (int i = 0; i < units.Length; i++)
            {
                units[i].GetComponent<Character>().reciveDamage(damage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(mobAgroPoint.position, agroDistance);
        Gizmos.DrawWireSphere(mobAttackPoint.position, mobAttackRange);
    }
}
