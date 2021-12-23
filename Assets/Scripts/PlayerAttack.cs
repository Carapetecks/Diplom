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
    
    private void Update()
    {

        if (timeBtwAttack <= 0)
        {

            if (Input.GetKeyDown(KeyCode.X))
            {
                Collider2D[] monsters = Physics2D.OverlapCircleAll(punchDot.position, attackRange, monster);
                for (int i = 0; i < monsters.Length; i++)
                {
                    monsters[i].GetComponent<Monster>().reciveDamage(damage);
                    timeBtwAttack = startTimeBtwAttack;
                }
            }
                     
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }


    }
   private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(punchDot.position, attackRange);
    }

   
}
