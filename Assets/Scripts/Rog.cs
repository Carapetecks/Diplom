using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rog : Monster
{
    public float speed;
    public float huntDistance;
    public float distToPlayer;
    public float mobAttackRangeX;
    public float mobAttackRangeY;
    private float currentTimeToAttack;
    private float currentTimeToShoot;
    public float timeToAttack = 1f;
    public float timeToShoot;

    private bool canAttack => currentTimeToAttack == 0;
    public bool faceRight = false;
    public bool isAgro = false;
    public bool isSearching;
    public bool isGrouded = false;
    public bool atHome;


    private Character character;
    private RogBullet bullet;
    private RogBullet rogbullet;

    public Transform homePoint;
    public Transform characterObject;
    public Transform mobAttackPoint;
    public Transform agroRadiusPoint;
    public Transform shootPoint;
    public Animator animator;
    public LayerMask characterLayer;
    Vector2 direction;

    private void Awake()
    {
        bullet = Resources.Load<RogBullet>("RogBullet");
        character = FindObjectOfType<Character>();
    }
    private void Start()
    {
        direction = transform.right;
        animator = GetComponentInChildren<Animator>();
   
    }
    private void Update()
    {
        if(lifes <= 0)
        {
            base.Die();
        }
        if (isAgro == true && currentTimeToShoot <= 0)
        {
            Shoot();
            currentTimeToShoot = timeToShoot;
        }

        if (CanSeePlayer(huntDistance))
        {
            isAgro = true;
        }
        else
        {
            if (isAgro)
            {
                if (!isSearching)
                {
                    isSearching = true;
                    Invoke("unAgro", 3f);
                    Invoke("MoveToHome", 5f);
                }
            }

        }

        if (isAgro == true)
        {
            Hunting();
        }
        
    } 
    private void FixedUpdate()
    {
 ///AttackTimer      
        if (isAgro == true && currentTimeToAttack > 0)
        {
            currentTimeToAttack -= Time.deltaTime;
        }
      
        if (isAgro == true && currentTimeToAttack <= 0)
        {
            MeleeAttack();           
            currentTimeToAttack = timeToAttack;
        }
///ShootTimer
        if (isAgro == true && currentTimeToShoot > 0)
            currentTimeToShoot -= Time.deltaTime;

    }


    public void MeleeAttack()
    {
        Collider2D[] units = Physics2D.OverlapBoxAll(mobAttackPoint.position, new Vector2(mobAttackRangeX, mobAttackRangeY), 0, characterLayer);
        if (units.Length > 0)
        {
            for (int i = 0; i < units.Length; i++)
            {
                units[i].GetComponent<Character>().reciveDamage(damage);
                
            }
        }
    }
    public void MoveToHome()
    {
        transform.position = homePoint.position;
        //if (faceRight)
        //{     
        //    transform.position = Vector3.MoveTowards
        //    (transform.position, homePoint.transform.position, speed * Time.deltaTime);
        //    transform.localScale = new Vector3(-1, 1, 1);
        //    animator.SetBool("Walk", true);
        //}
        //else if (!faceRight)
        //{ 
        //    transform.localScale = new Vector3(1, 1, 1);
        //    transform.position = Vector3.MoveTowards
        //   (transform.position, homePoint.transform.position, speed * Time.deltaTime);
        //    animator.SetBool("Walk", true);
        //}
        //if (transform.position.x == homePoint.position.x)
        //{
        //    rigidbody.velocity = new Vector2(0,0);
        //    animator.SetBool("Walk", false);
        //}
    }
    public void Shoot()
    {   
        Vector3 position = shootPoint.position;
        RogBullet newBullet = Instantiate
          (bullet, position, bullet.transform.rotation) as RogBullet;
        if (faceRight == false)
        {   
            newBullet.Direction = newBullet.transform.right * (sprite.flipX ? 1.0f : -1.0f);
        }
        else if(faceRight == true)
        {
            newBullet.Direction = - newBullet.transform.right * (sprite.flipX ? 1.0f : -1.0f);
        }
    }
       

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        var castDist = distance;
        if(characterObject.position.x < transform.position.x)
        {
            castDist = - distance;
        }
        Vector2 endPos = agroRadiusPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(agroRadiusPoint.position, endPos, 1 << LayerMask.NameToLayer("character"));
        
        if (hit.collider !=null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }

            Debug.DrawLine(agroRadiusPoint.position, hit.point, Color.magenta);
        }
        else
            Debug.DrawLine(agroRadiusPoint.position, endPos, Color.green);
       
        return val;
       
    }

    public void Hunting()
    { 
        if (characterObject.position.x < transform.position.x)
        {
            faceRight = true;
            rigidbody.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector3(1, 1, 1);
            bullet.transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("Walk", true);
        }
        else if (characterObject.position.x > transform.position.x)
        {
           
            faceRight = false;
            rigidbody.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector3(-1, 1, 1);
            bullet.transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("Walk", true);
        }
    }

    public void unAgro()
    {
        isAgro = false;
        isSearching = false;
        animator.SetBool("Walk", false);
        animator.SetBool("Stay", true);
        rigidbody.velocity = new Vector2(0, 0);
    }

    public void DistanceCount()
    {
        distToPlayer = Vector2.Distance(agroRadiusPoint.position, characterObject.position);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(mobAttackPoint.position, new Vector3(mobAttackRangeX, mobAttackRangeY, 0));
    }

    IEnumerator SearchCoroutine()
    {
        rigidbody.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(2f);
        Hunting();
    }
}
