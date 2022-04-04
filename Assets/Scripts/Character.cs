using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Character : Unit
{
    
    public float speed = 2.0f;
    private float jumpForce = 5.0f;
    private float dashForce = 3f;
    float dirX, dirY;
    public float boxX;
    public float boxY;
    public float wallBoxX;
    public float wallBoxY;

    private bool isGrounded = false;
    private bool isClimbing;
    private bool isDashing;
    public bool faceRight = true;
    
    public int numOfHeart;
    
    private float timeBtwDash;
    public float startTimeBtwDash;

    public Transform groundCheckPoint, wallCheckPoint;
    public LayerMask ground, wall;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;   
    public Animator animator;
    Vector2 moveVecX;
    Vector3 direction;




    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }   

    private void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, new Vector2(boxX, boxY), 0, ground);
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");
        
///JUMP
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
             Jump();
        }

///DASH
       if (timeBtwDash <= 0)
        { 
            if (Input.GetButtonDown("Dash") && animator)
            {
                animator.SetTrigger("Dash");
                Dash();            
            }        
        }
       
        if (Input.GetKeyDown(KeyCode.H)) CheatHeal();
       
    }

    private void FixedUpdate()
    {
       
        if(!isDashing)
        {
           Run(); 
        }

        if (timeBtwDash > 0)
        {
            timeBtwDash -= Time.deltaTime;
        }
        else if (isDashing)
        {
            isDashing = false;
            if (isDashing == false)
                Debug.Log(isDashing);
        }
        if(!isDashing)
        Reflect();    
        HeatPoint();        
    }

    private void Run()
    {       
        moveVecX = new Vector2(dirX * speed, rigidbody.velocity.y);
        rigidbody.velocity = moveVecX;
       
        if (dirX != 0 && isGrounded)
        {
            animator.SetBool("Run", true);
            
        }
        else
        {
            animator.SetBool("Run", false);        
        }
    }
   

    void Reflect()
    {
        if ((dirX > 0 && !faceRight) || (dirX < 0 && faceRight))
        {
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
            faceRight = !faceRight;
        }
    }

    public override void reciveDamage(int damage)
    {
        base.reciveDamage(damage);
        Kick(GetComponent<Character>());
        Debug.Log(lifes);
        if (lifes <= 0)
        {         
            base.Die(); 
            SceneManager.LoadScene("Menu");
        }     
    }

    private void Jump() // ПОЧИНИТЬ ИНЕРЦИЮ ПРЫЖКОВ
    { 
        rigidbody.velocity = new Vector2(0, 0);
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
       
    }
  
     private void Dash() // ПЕРЕДЕЛАТЬ
    {
        if (!faceRight)
        {
            isDashing = true;
            rigidbody.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
            timeBtwDash = startTimeBtwDash;

        }
        else if (faceRight)
        {
            isDashing = true;
            rigidbody.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
            timeBtwDash = startTimeBtwDash;
        }
        if (isDashing == true)
            Debug.Log(isDashing);
    }


    
    private void OnCollisionEnter2D(Collision2D collision)
    {      
        if (collision.gameObject.tag.Equals("DroppedTrap"))
        {
            reciveDamage(1);
        }       
    }
    private void HeatPoint()
    {
        if (lifes > numOfHeart)
        {
            lifes = numOfHeart;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(lifes))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    private void Kick(Character character) // нужна переменная направления
    {
        rigidbody.velocity = Vector3.zero;
        if (character.faceRight && rigidbody.isKinematic == false)
        {
            rigidbody.AddForce(transform.up * 2.5f + transform.right + (-direction) * 2.5f, ForceMode2D.Impulse);

        }
        else if (!character.faceRight && rigidbody.isKinematic == false)
        {
            rigidbody.AddForce(transform.up * 2.5f + -transform.right + (-direction) * 2.5f, ForceMode2D.Impulse);

        }
    }

    private void CheatHeal()
    {
        lifes++;
    }
    
    public void SaveCharacter()
    {
        SaveSystem.SaveCharacter(this);
    }
    public void LoadCharacter()
    {
        CharacterData data = SaveSystem.LoadCharacter();
        lifes = data.lifes;

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];
        transform.position = position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheckPoint.position, new Vector3(boxX, boxY, 0));
        Gizmos.DrawWireCube(wallCheckPoint.position, new Vector3(wallBoxX, wallBoxY, 0));
       
    }


}








