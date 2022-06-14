using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;

public class Character : Unit
{
    
    public float speed = 2.0f;
    public float jumpForce = 5.5f;
    private float dashForce;
    private float dashForceEnd = 0f;  
    private float wallSlidingSpeed = -0.2f;  
   
    public float boxX;
    public float boxY;
    public float wallBoxX;
    public float wallBoxY;
    public float itemBoxX;
    public float itemBoxY;
    
    float normalGravity;
    float dirX, dirY;

    private bool isGrounded = true;
    private bool isClimbing;
    private bool isSliding = false;
    private bool isDashing;
    private bool isJumping = false;
    public bool faceRight = true;
   
    public bool canTake;
    public bool canTalk;
    public bool canTakeScore;
    public bool canGo;

    
    public int numOfHeart;
    public int maxClimbingJumpAmount = 1;
    public int climbingJumpAmount;
    
    private float timeBtwDash;
    public float startTimeBtwDash;

    public Transform groundCheckPoint, wallCheckPoint, itemCheckPoint;
    public LayerMask ground, wall, item, character, NPC, scoreCrystall, transition;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;   
    public Animator animator;
    public GameObject FRight;
    public GameObject FLeft;
    public GameObject Dead;
    public GameObject Interface;
    public GameObject parryObject;
    public GameObject follower;
    public ParticleSystem parryEffect;
    Vector2 moveVecX;
    Vector3 direction;




    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        normalGravity = rigidbody.gravityScale;
        if (DialogueManager.isQuestDone == true)
        {
            follower.SetActive(true);
        }

    }   

    private void Update()
    {
///DEFINING FIELDS
        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, new Vector2(boxX, boxY), 0, ground);
        isClimbing = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(wallBoxX, wallBoxY), 0, wall);
        
        canTake = Physics2D.OverlapBox(itemCheckPoint.position, new Vector2(itemBoxX, itemBoxY), 0, item);
        canTalk = Physics2D.OverlapBox(itemCheckPoint.position, new Vector2(itemBoxX, itemBoxY), 0, NPC);
        canTakeScore = Physics2D.OverlapBox(itemCheckPoint.position, new Vector2(itemBoxX, itemBoxY), 0, scoreCrystall);
        canGo = Physics2D.OverlapBox(itemCheckPoint.position, new Vector2(itemBoxX, itemBoxY), 0, transition);
        
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");

///JUMP
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("JumpDown");
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && isClimbing && climbingJumpAmount >=1)
        {
           
            rigidbody.velocity = Vector2.zero;
            Jump();
            climbingJumpAmount--;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            follower.SetActive(true);
        }           

///DASH
        if (timeBtwDash <= 0)
        {
            if (Input.GetButtonDown("Dash") && animator && !isClimbing)
            {              
                StartCoroutine("DashCoroutine");
                animator.SetTrigger("Dash");
                Dash();                         
            }        
        }
       
        if (Input.GetKeyDown(KeyCode.H)) CheatHeal();
        
        if (!isClimbing && climbingJumpAmount < maxClimbingJumpAmount)
            climbingJumpAmount = maxClimbingJumpAmount;
        
        ItemSearch();

    }

    private void FixedUpdate()
    {
        HeatPoint();
///RUN        
        if (!isDashing)
        {
            Run();
        }
 ///DASH TIMER
        if (timeBtwDash > 0)
        {
            timeBtwDash -= Time.deltaTime;
        }
///SLIDING
        if (isClimbing && !isGrounded && rigidbody.velocity.y <= 0 && dirY >= 0 && dirX != 0)
        {
            animator.SetBool("Climbing", true);
            isSliding = true;
            Vector2 velocity = rigidbody.velocity;
            velocity.y = wallSlidingSpeed;
            rigidbody.velocity = velocity;
        }
        else
            animator.SetBool("Climbing", false);

///REFLECT CHARACTER DIRECTION      
        if (!isDashing)
        Reflect();

       

    }

    private void Run()
    {       
        moveVecX = new Vector2(dirX * speed, rigidbody.velocity.y);
        rigidbody.velocity = moveVecX;
       
        if (dirX != 0 && isGrounded && !isDashing)
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
            ScoreText.Score = 0;
            base.Die();
            
        }
    }

    private void Jump()
    {
        if (!isDashing)
        {
            isJumping = true;
            rigidbody.velocity = new Vector2(0, 0);
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }      
    }
    private void Dash() 
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
    IEnumerator DashCoroutine()
    {
        Vector2 originalVelocity = new Vector2(rigidbody.velocity.x, 0);
        dashForce = 6f;
        isDashing = true;
        rigidbody.gravityScale = 0;
        rigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.3f);
        rigidbody.gravityScale = normalGravity;
        rigidbody.velocity = originalVelocity;
        isDashing = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("DroppedTrap"))
        {
            reciveDamage(1);
        }
        else if (collision.gameObject.tag.Equals("DeadZone"))
            reciveDamage(100);
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
        if (character.faceRight)
        {
            rigidbody.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
        }
        else if (!character.faceRight)
        {
            rigidbody.AddForce(Vector2.up * 2, ForceMode2D.Impulse);
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
    public void ItemSearch()
    {
        if(canTake == true || canTalk==true || canTakeScore==true || canGo == true)
        {
            if (faceRight)
            {
                FRight.SetActive(false);
                FLeft.SetActive(true);
            }          
            else
            {
                FLeft.SetActive(false);
                FRight.SetActive(true);
            }
        }
        else
        { 
            FLeft.SetActive(false);
            FRight.SetActive(false);
        }
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheckPoint.position, new Vector3(boxX, boxY, 0));
        Gizmos.DrawWireCube(wallCheckPoint.position, new Vector3(wallBoxX, wallBoxY, 0));
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(itemCheckPoint.position, new Vector3(itemBoxX, itemBoxY, 0));
    }
    public void SpeedTimer()
    {
        StartCoroutine(Speed());
    }
    IEnumerator Speed()
    {
        yield return new WaitForSeconds(5f);
        speed -= 2f;
    }

    /// IGNOR ITEMS LAYER
    public void IgnorLayer()
    {
        Physics2D.IgnoreLayerCollision(9, 10);
    }

    public void ForceDamageTimer()
    {
        StartCoroutine(ForcePower());
    }
    IEnumerator ForcePower()
    {
        yield return new WaitForSeconds(5f);
        damage -= 1;
    }


}








