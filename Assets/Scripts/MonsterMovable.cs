using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterMovable : Monster
{
    [SerializeField] private float speed = 1.0f; 
    [SerializeField] private float timeToAttack = 1f;
    private float currentTimeToAttack = 0;
    private bool canAttack => currentTimeToAttack == 0;   
    private Character mainCharacter;
    Vector3 direction;  
    public bool faceRight = true;
    public bool isGrounded = false;
    public MonsterMovable() :base()
    {

    }


    protected void FixedUpdate()
    {
        CheckGround();
    }
    
    protected override void Start()
    {
        direction = transform.right;
    }
    protected override void Update()
    {        
        if (currentTimeToAttack != 0)
            currentTimeToAttack -= Time.deltaTime;
        if (currentTimeToAttack < 0)
            currentTimeToAttack = 0;
        if (mainCharacter && Vector2.Distance(transform.position, mainCharacter.transform.position) > 1)
            currentTimeToAttack = 0;       
        if (isGrounded) Move();        
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position+ transform.up * 0.5f + transform.right * direction.x * 0.35f,  0.01f);
        if (colliders.Length > 0 && colliders.All(x => !x.GetComponent<Character>())
            && colliders.All(x => !x.GetComponent<StaticMonster>())) direction *= -1.0f;         
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        //sprite.flipX = direction.x > 0.0f;
        if((direction.x < 0 && !faceRight ) || (direction.x > 0 && faceRight))
        { 
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
            faceRight = !faceRight;
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D collider)
    {         
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Character && canAttack)
            AttackCharacter((Character)unit);
    }
    private void AttackCharacter(Character character)
    { 
        mainCharacter = character;  
        mainCharacter.reciveDamage(damage); 
        currentTimeToAttack = timeToAttack;
    }
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = colliders.Length > 1;
    }
   

}
