using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : Unit
{
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float jumpForce = 5.0f;
    [SerializeField]
    private int lifes = 5;
    private bool isGrounded = false;
    Vector3 direction;
    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
    }
    private void Run()
    {
        direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0f;
        
    }
    public override void reciveDamage()
    {
        
        lifes--;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 2.5f +transform.right+(-direction) * 2.5f , ForceMode2D.Impulse);
        Debug.Log(lifes);
        if (lifes <= 0)
        {         
            base.reciveDamage();
            Debug.Log("You are dead 8====0");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }

    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
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
            reciveDamage();
        }
        //collision.gameObject.name.Equals("Character")
    }
}
