using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public Transform playerCheck;
    public LayerMask groundObjects;
    public LayerMask playerobject;
    public LayerMask test;
    public float checkRadius;
    public int maxJumpCount;

    private Rigidbody2D rb;
    private bool facingRight =true;
    private float moveDirection;
    private bool isJumping = false ;
    private bool isGrounded;
    private bool isPlayer;
    private int jumpCount;

    private void Awake()
    {
        rb =GetComponent<Rigidbody2D>();
    }
    private void Start(){
        jumpCount=maxJumpCount;
    }


    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }
    private void FixedUpdate(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        isPlayer =Physics2D.OverlapCircle(playerCheck.position, checkRadius, playerobject);
        if(isGrounded || isPlayer){
            jumpCount=maxJumpCount;
        }
        Move();
    }
    private void Move(){
        rb.velocity =new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if(isJumping){
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
        }
        isJumping = false;
    }
    private void ProcessInputs(){
        moveDirection = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && jumpCount>0){
            isJumping =true;
        }
    }
}
