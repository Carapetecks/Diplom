using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //private Unit unit;
    //private Character character;
    //private new Rigidbody2D rigidbody;
    //Vector3 direction;

    public virtual void reciveDamage()
    {  
        //if(unit && unit is Character)
        //{
        //    unit.direction = direction;
        //    unit.rigidbody.velocity = Vector3.zero;
        //    unit.rigidbody.AddForce(transform.up * 2.5f + transform.right + (-direction) * 2.5f, ForceMode2D.Impulse);
        //}

    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
  
}
