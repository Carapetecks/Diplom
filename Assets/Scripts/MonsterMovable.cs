using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterMovable : Monster
{
    [SerializeField]
    private float speed = 1.0f;
    Vector3 direction;
    private SpriteRenderer sprite;

    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    protected override void Start()
    {
        direction = transform.right;
    }
    protected override void Update()
    {
        Move();
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position+ transform.up * 0.5f + transform.right * direction.x * 0.35f,  0.01f);
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Character>())) direction *= -1.0f; 
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x > 0.0f;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if(unit && unit is Character)
        {
            unit.reciveDamage();
        }
    }
}
