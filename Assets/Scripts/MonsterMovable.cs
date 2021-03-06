using UnityEngine;
using System.Linq;

public class MonsterMovable : Monster
{
    public float speed = 1.0f; 
    [SerializeField] private float timeToAttack = 1f;
    private float currentTimeToAttack = 0;
    public float mobAttackRange;      
    private bool canAttack => currentTimeToAttack == 0;   
    public bool faceRight = true;
    public bool isGrounded = false;
    public Transform mobAttackDot;
    private Character mainCharacter;
    public LayerMask character;
    Vector3 direction;
    public MonsterMovable() :base()
    {

    }

    protected void FixedUpdate()
    {
        if (currentTimeToAttack > 0)
        {
            currentTimeToAttack -= Time.deltaTime;
        }
        CheckGround();
    }
    
    protected override void Start()
    {
        direction = transform.right;
        //InvokeRepeating("MonsterAttack", 1, 0.8f);
    }
    protected override void Update()
    {        
        if(currentTimeToAttack<=0)
        {
            MonsterAttack();
            currentTimeToAttack = timeToAttack;
        }

        //if (mainCharacter && Vector2.Distance(transform.position, mainCharacter.transform.position) > 1)
        //    currentTimeToAttack = 0;
        if (isGrounded) Move();
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position+ transform.up * 0.3f + transform.right * direction.x * 0.35f,  0.01f);
        if (colliders.Length > 0 
            && colliders.All(x => !x.GetComponent<Character>())
            && colliders.All(x => !x.GetComponent<StaticMonster>()
            && colliders.All(x => !x.GetComponent<LowAttackShootingMonster>())
            && colliders.All(x => !x.GetComponent<FallenTrap>()))) direction *= -1.0f;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        if((direction.x < 0 && !faceRight ) || (direction.x > 0 && faceRight))
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
        if(units.Length > 0)
        {
            for (int i = 0; i < units.Length; i++)
            {
                units[i].GetComponent<Character>().reciveDamage(damage);
            }
        }    
    } 
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = colliders.Length > 1;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(mobAttackDot.position, mobAttackRange);
    }
   
}
