using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Character : Unit
{
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float jumpForce = 5.0f;

    private bool isGrounded = false;
    public bool faceRight = true;    
    Vector3 direction;
    
    

    

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        Reflect();

    }
    private void Run()
    {
        direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);             
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

    

    
   
        
    
}
