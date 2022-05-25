using UnityEngine;
using System.Linq;

public class Bullet : MonoBehaviour
{
    public float speed = 1.0f;
    private SpriteRenderer sprite;   
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }
    
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards
            (transform.position, transform.position + direction, speed * Time.deltaTime);
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.right * direction.x * 0.09f, 0.01f);
        if (colliders.Length > 0 
            && colliders.All(x => !x.GetComponent<Monster>())
            && colliders.All(x => !x.GetComponent<Item>())
            && colliders.All(x => !x.GetComponent<Character>()) 
            && colliders.All(x => !x.GetComponent<FallenTrap>())
            && colliders.All(x => !x.GetComponent<WalkTip>()))
        {
            Destroy(gameObject);
        }
    }

}
