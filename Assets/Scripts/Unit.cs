using System.Collections;
using UnityEngine;


public class Unit : MonoBehaviour
{
    [SerializeField]
    public int lifes;
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
        StartCoroutine("DamageIdentification");
        if(lifes<=0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }  
        
    IEnumerator DamageIdentification()
    {
        sprite.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(255, 255, 255);
    }

    
}
