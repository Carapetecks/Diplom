using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform punchDot;
    public LayerMask monster;
    public float attackRange;
    public int damage;
    private SpriteRenderer sprite;
    private Character mainCharacter;
    Vector3 direction;
    
    
    private void Update()
    {
        Attack();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(punchDot.position, attackRange);
    }
    private void Kick(Monster monster)
    {
        var rigid = monster.GetComponent<Rigidbody2D>();
        mainCharacter = GetComponent<Character>();
        rigid.velocity = Vector3.zero;       
        if (mainCharacter.faceRight && rigid.isKinematic == false)
        { 
            rigid.AddForce(transform.up * 2.5f + transform.right + (-direction) * 2.5f, ForceMode2D.Impulse);
            StartCoroutine(MoveStoped(monster.GetComponent<MonsterMovable>()));
        }
        else if(!mainCharacter.faceRight && rigid.isKinematic == false)
        {
            rigid.AddForce(transform.up * 2.5f + -transform.right + (-direction) * 2.5f, ForceMode2D.Impulse);
            StartCoroutine(MoveStoped(monster.GetComponent<MonsterMovable>()));
        }
        
    }
    
    private void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Collider2D[] monsters = Physics2D.OverlapCircleAll(punchDot.position, attackRange, monster);
                for (int i = 0; i < monsters.Length; i++)
                {
                    monsters[i].GetComponent<Monster>().reciveDamage(damage);
                    Kick(monsters[i].GetComponent<Monster>());
                    timeBtwAttack = startTimeBtwAttack;
                }
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    IEnumerator MoveStoped(MonsterMovable monster)
    {
        float maxSpeed = monster.speed;
        monster.speed = 0;
        yield return new WaitForSeconds(0.5f);
        monster.speed = 1;
        
    }



}
