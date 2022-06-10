using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rog : Monster
{
    public float speed;
    public float huntDistance;
    public float distToPlayer;

    public bool faceRight = true;
    public bool isAgro = false;

    private Bullet bullet;
    public Transform character;
    public Transform agroRadiusPoint;
    public Transform shootPoint;
    public Animator animator;

    private void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet");
    }
    private void Start()
    {
        InvokeRepeating("Shoot", 0, 2f);
        animator = GetComponentInChildren<Animator>();
        isAgro = false;
    }
    private void Update()
    {
       
        DistanceCount();
        Debug.Log("distToPlayer");
        if (isAgro == false) 
        { 
            animator.SetBool("Walk", false);
            animator.SetBool("Stay", true);
        }
        else
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Stay", false);
        }
    }
    private void FixedUpdate()
    {

        if(distToPlayer < huntDistance)
        {
            Hunting();
            isAgro = true;
        }
        else
        {
            isAgro = false;
        }
        

    }
    public void MoveToHome()
    {
       
    }
    public void Shoot()
    {
        Vector3 position = shootPoint.position;
        Bullet newBullet = Instantiate
          (bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.transform.position = character.transform.position * 1;
    }

    public void Hunting()
    {
        if (character.position.x < transform.position.x)
        {
            rigidbody.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (character.position.x > transform.position.x)
        {
            rigidbody.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }

       
    }

    public void unAgro()
    {

    }

    public void DistanceCount()
    {
        distToPlayer = Vector2.Distance(transform.position, character.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(agroRadiusPoint.position, huntDistance);
    }
}
