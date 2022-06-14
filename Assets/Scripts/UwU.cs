using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UwU : Monster
{
    public float speed = 1.0f;
    public float lastSpeed;
    [SerializeField] private float timeToAttack = 1f;
    private float currentTimeToAttack = 0;
    public float mobAttackRange;

    public float groundBoxX;
    public float groundBoxY;
    public float wallBoxX;
    public float wallBoxY;
    private bool canAttack => currentTimeToAttack == 0;
    public bool faceRight = true;
    public bool isGrounded = true;
    public bool canWalkGround = false;


    public Transform mobAttackDot, groundCheckPoint, collidersCheckPoint;
    private Character mainCharacter;
    public LayerMask character, ground, wall, monster, item, scoreCrystall;
    Vector3 direction;
    public UwU() : base()
    {

    }

    protected void FixedUpdate()
    {
        if (currentTimeToAttack > 0)
        {
            currentTimeToAttack -= Time.deltaTime;
        }

        if (isGrounded) Move();

        if (currentTimeToAttack <= 0)
        {
            MonsterAttack();
            currentTimeToAttack = timeToAttack;
        }
    }

    protected override void Start()
    {
        direction = transform.right;

    }
    protected override void Update()
    {

        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, new Vector2(groundBoxX, groundBoxY), 0, ground);
    }

    private void Move()
    {
        rigidbody.velocity = new Vector2(-speed, 0);
        Collider2D[] groundColliders = Physics2D.OverlapBoxAll(collidersCheckPoint.position, new Vector2(wallBoxX, wallBoxY), 0, ground);
        Collider2D[] monstersColliders = Physics2D.OverlapBoxAll(collidersCheckPoint.position, new Vector2(wallBoxX, wallBoxY), 0, monster);
        Collider2D[] wallsColliders = Physics2D.OverlapBoxAll(collidersCheckPoint.position, new Vector2(wallBoxX, wallBoxY), 0, wall);
        Collider2D[] itemsColliders = Physics2D.OverlapBoxAll(collidersCheckPoint.position, new Vector2(wallBoxX, wallBoxY), 0, item);
        Collider2D[] scoreCollieders = Physics2D.OverlapBoxAll(collidersCheckPoint.position, new Vector2(wallBoxX, wallBoxY), 0, scoreCrystall);
        if (groundColliders.Length > 0
            || wallsColliders.Length > 0
            || itemsColliders.Length > 0
            || scoreCollieders.Length > 0)
        {
            direction *= -1;
            speed *= -1;
            lastSpeed = speed;
        }
        if (monstersColliders.Length > 0
            && monstersColliders.All(x => !x.GetComponent<StaticMonster>())
            && monstersColliders.All(x => !x.GetComponent<LowAttackShootingMonster>()))
        {
            direction *= -1;
            speed *= -1;
            lastSpeed = speed;
        }

        if ((direction.x > 0 && !faceRight) || (direction.x < 0 && faceRight))
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
            faceRight = !faceRight;
        }
    }

    private void MonsterAttack()
    {
        Collider2D[] units = Physics2D.OverlapCircleAll(mobAttackDot.position, mobAttackRange, character);
        if (units.Length > 0)
        {
            for (int i = 0; i < units.Length; i++)
            {
                units[i].GetComponent<Character>().reciveDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(groundCheckPoint.position, new Vector3(groundBoxX, groundBoxY, 0));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(collidersCheckPoint.position, new Vector3(wallBoxX, wallBoxY, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(mobAttackDot.position, mobAttackRange);

    }

}
