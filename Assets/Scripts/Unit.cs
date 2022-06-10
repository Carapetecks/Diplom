using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
  
    [SerializeField]
    public int lifes;
    public int damage;
    public Rigidbody2D rigidbody;
    private Animator animator;
    public SpriteRenderer sprite;
    Vector3 direction;
   

    public void Awake()
    {

        sprite = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
       
    }

    public virtual void reciveDamage(int damage)
    {
        Unit unit = GetComponent<Unit>();
        lifes -= damage;
        StartCoroutine("DamageIdentification");
        if(lifes<=0)
        {
            if(unit is LowAttackShootingMonster)
            {
                ScoreText.Score += 100;
                Die();
            }else if(unit is MonsterMovable)
            {
                ScoreText.Score += 150;
                Die();
            }else if(unit is StaticMonster)
            {
                ScoreText.Score += 75;
                Die();
            }else if (unit is UwU)
            {
                ScoreText.Score += 50;
                Die();
            }            
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
