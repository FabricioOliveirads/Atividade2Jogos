using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D rigidbody;
    public float JumpForce;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isGrounded;
    private float timeAttack;
    public float startTimeAttack;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rigidbody.velocity = new Vector2(-Speed, rigidbody.velocity.y);
            animator.SetBool("IsWalking",true);
            sprite.flipX = true;
        }
        else
        {
            rigidbody.velocity = new Vector2(0,rigidbody.velocity.y);
            animator.SetBool("IsWalking", false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.velocity = new Vector2(Speed, rigidbody.velocity.y);
            animator.SetBool("IsWalking", true);
            sprite.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)&& isGrounded==true)
        {
            rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("IsJumping",true);
        }
        if(timeAttack <= 0)
        {            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                animator.SetTrigger("IsAttacking");
                timeAttack = startTimeAttack;
            }           
        }
        else
        {
            timeAttack -= Time.deltaTime;
            animator.SetTrigger("IsAttacking");
        }
    }
     void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 8)
        {
            isGrounded = true;
            animator.SetBool("IsJumping",false);
        }
    }

}
