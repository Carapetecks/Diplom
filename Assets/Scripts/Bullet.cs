using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Start()
    {
        
        Destroy(gameObject, 1.0f);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards
            (transform.position, transform.position + direction, speed * Time.deltaTime);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit != null)
        {
            unit.reciveDamage();        

        }
        
    }
}
