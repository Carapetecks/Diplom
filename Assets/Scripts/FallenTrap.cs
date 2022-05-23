using UnityEngine;


public class FallenTrap : MonoBehaviour
{
    Rigidbody2D rigidbody;
    
  
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Character)
        {
            rigidbody.isKinematic = false;
            
        }
        if(rigidbody.isKinematic == false)
        {
            Destroy(gameObject, 1f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.name.Equals("Character"))
        {
            Destroy(gameObject);
        }
        //if (collision.gameObject.name.Equals("Character"))
        //{          
        //    Destroy(gameObject, 0.2f);
        //}
        //collision.gameObject.name.Equals("Character")
    }
    
}
