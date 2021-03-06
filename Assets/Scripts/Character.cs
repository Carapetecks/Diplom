using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : Unit
{
    public Pause_menu menu;
    private Spawn spawn;
    [SerializeField]    
    public float speed = 2.0f;
    [SerializeField]
    private float jumpForce = 5.0f;
    private float dashForce = 3.5f;
    private bool isGrounded = false;
    public bool faceRight = true;
    Vector3 direction;
    public int numOfHeart;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;    
    private float timeBtwDash;
    public float startTimeBtwDash;
    private int level;
    //float direction;

    
    private void FixedUpdate()
    {
        
        CheckGround();
        HeatPoint();
    }
    private void Awake()
    {
        menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<Pause_menu>();
    }
    private void Update()
    {
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        if (timeBtwDash<=0)
        {
            if (Input.GetButtonDown("Dash"))
            {
                Dash();
                timeBtwDash = startTimeBtwDash;
            }
        }        
        else
        {
            timeBtwDash -= Time.deltaTime;
        }       
        Reflect();
    }
    
    private void Run()
    {
        direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        //direction = Input.GetAxis("Horizontal");
        //Vector2 moveVecX = new Vector2(direction * speed, rigid.velocity.y);
        //rigid.velocity = moveVecX;
    }
    void Reflect()
    {
        if ((direction.x > 0 && !faceRight) || (direction.x < 0 && faceRight))
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

    private void Jump()
    {
        rigid.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
   
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        isGrounded = colliders.Length > 1;
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
    private void Kick(Character character)
    {
        rigid.velocity = Vector3.zero;
        if (character.faceRight && rigid.isKinematic == false)
        {
            rigid.AddForce(transform.up * 2.5f + transform.right + (-direction) * 2.5f, ForceMode2D.Impulse);
            
        }
        else if (!character.faceRight && rigid.isKinematic == false)
        {
           rigid.AddForce(transform.up * 2.5f + -transform.right + (-direction) * 2.5f, ForceMode2D.Impulse);
            
        }
    }

    private void Dash()
    {
        if (faceRight)
        {
            rigid.AddForce(transform.right * dashForce, ForceMode2D.Impulse);
            timeBtwDash = startTimeBtwDash;
        }
        else if (!faceRight)
        {
            rigid.AddForce(-transform.right * dashForce, ForceMode2D.Impulse);
            timeBtwDash = startTimeBtwDash;
        }        
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
    public void SpeedTimer()
    {
        StartCoroutine(Speed());
    }
    IEnumerator GravityScaleDrop()
    {
        yield return new WaitForSeconds(1f);
    }    
    IEnumerator Speed()
    {        
        yield return new WaitForSeconds(5f);        
        speed -= 2f;
    }

}








