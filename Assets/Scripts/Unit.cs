using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
  
    [SerializeField]
    public int lifes;
    public int damage = 1;
    public Rigidbody2D rigidbody;
    private Animator animator;
    public SpriteRenderer sprite;
    Vector3 direction;
   

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void reciveDamage(int damage)
    {
        lifes -= damage;
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
