using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTmove : Unit
{
    public float speed;
    private float jumpForce;
    float dirX, dirY;
    bool isGrounded;
    public Transform groundCheckPoint;
    public float boxX;
    public float boxY;
    public LayerMask ground;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, new Vector2(boxX, boxY), 0, ground);
        dirX = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        if(isGrounded) Run();
       
    }
    private void Run()
    {
        Vector2 movVecX = new Vector2(dirX * speed, rigidbody.velocity.y);
        rigidbody.velocity = movVecX;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheckPoint.position, new Vector3(boxX, boxY, 0));
    }

}
