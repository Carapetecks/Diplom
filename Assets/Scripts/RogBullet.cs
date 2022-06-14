using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RogBullet : MonoBehaviour
{
    public float speed = 1.0f;
    public float colliderCheckPointRadius;
    private bool faceRight = false;


    public Transform colliderCheckPoint;
    private SpriteRenderer sprite;
    public Character character;
   
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        character = FindObjectOfType<Character>();
    }

    private void FixedUpdate()
    {
       
        transform.position = Vector3.MoveTowards
            (transform.position, character.transform.position, speed * Time.deltaTime);
        BulletColliderCheck();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        if (character != null)
        {
            character.reciveDamage(1);
            Destroy(gameObject);
        }
    }

    protected void BulletColliderCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + colliderCheckPoint.position * direction.x * 0.09f, colliderCheckPointRadius);
        if (colliders.Length > 0
            && colliders.All(x => !x.GetComponent<Monster>())
            && colliders.All(x => !x.GetComponent<RogBullet>())
            && colliders.All(x => !x.GetComponent<Rog>())
            && colliders.All(x => !x.GetComponent<Item>())
            && colliders.All(x => !x.GetComponent<ScoreCrystall>())
            && colliders.All(x => !x.GetComponent<Character>())
            && colliders.All(x => !x.GetComponent<FallenTrap>())
            && colliders.All(x => !x.GetComponent<WalkTip>()))
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(colliderCheckPoint.position, colliderCheckPointRadius);
    }
}
