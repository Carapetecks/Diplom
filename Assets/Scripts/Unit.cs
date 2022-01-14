using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //private Unit unit;
    //private Character character;
    //private new Rigidbody2D rigidbody;
    //Vector3 direction;
    [SerializeField]
    public int lifes = 5;
    public int damage = 1;
    public Rigidbody2D rigid;
    private Animator animator;
    public SpriteRenderer sprite;
    Vector3 direction;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void reciveDamage(int damage)
    {
        lifes -= damage;
        //rigid.velocity = Vector3.zero;
        //rigid.AddForce(transform.up * 2.5f + transform.right + (-direction) * 2.5f, ForceMode2D.Impulse);
        StartCoroutine("DamageIdentification");
        if(lifes<=0)
        {
            Die();
        }
    }

    IEnumerator DamageIdentification()
    {
        sprite.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(255, 255, 255);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    
}
