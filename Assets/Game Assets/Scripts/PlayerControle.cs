using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControle : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb; 

    public float jumpforce;

    public float radius;

    private bool Grounded;
    private Collider2D[] GroundCheck;
    public float positionFeet;
    public float rayFeet;

    public Animator animator;

    private bool facingRight = true;
    public bool Controle = false;
    public bool Animating = false;

    //Cheat God mod 
    public bool GodMod = false;

    private void Start()
    {
        animator.SetTrigger("Spawn");
        GodMod = false;
    }
    private void FixedUpdate()
    {
        if (!Animating && !GodMod)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
            animator.SetFloat("VelocityY", rb.velocity.y);
            animator.SetBool("Grounded", Grounded);
        }
    }
    void Update()
    {
        groundCheck();
        //Animation Script
        if (!Grounded){
           animator.SetBool("IsJumping",(rb.velocity.y>0.01));
        }
        animator.SetBool("IsFalling",(rb.velocity.y<-0.01) && !Controle);
        animator.SetBool("IsControlling", Controle);
        if (Input.GetMouseButtonDown(0) && Time.timeScale!=0f)
        {
            animator.SetTrigger("Shoot");
        }

        //Movement Script

        if (!Controle && !Animating)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
            
        }

        if (Input.GetAxis("Horizontal")>0.1)
        {
            if (!facingRight){Flip();}
        }
        else if (Input.GetAxis("Horizontal")<-0.1)
        {
            if (facingRight){Flip();}
        }
        if (Input.GetButtonDown("Jump") && Grounded)
        {
            rb.velocity=new Vector2(0,jumpforce);
        }
        if (Input.GetMouseButtonDown(2))
        {
            Controle = true;
            rb.velocity =new Vector2 (0,rb.velocity.y);
        }
        if (Input.GetMouseButtonUp(2))
        { 
            Controle = false; 
        }

        //Cheat

        /*if (Input.GetKeyDown(KeyCode.F9))
        {
            GodMod=!GodMod;
            GetComponent<CapsuleCollider2D>().enabled = !GodMod;
            if(GodMod) 
            {
                rb.gravityScale = 0f;
                speed = 10f;
            }
            else 
            {
                rb.gravityScale = 1.8f;
                speed = 6f;
            }

        }
        if (GodMod)
        {
            rb.velocity = new Vector2(rb.velocity.x,Input.GetAxis("Vertical") * speed);
        }
        if(GodMod && Input.GetKeyDown(KeyCode.F11)) { speed++; }
        if(GodMod && Input.GetKeyDown(KeyCode.F12)) { speed--; }*/
    }

    void groundCheck()
    {
        Grounded = false;
        GroundCheck = Physics2D.OverlapCircleAll(transform.position + Vector3.up * positionFeet, rayFeet);
        foreach (Collider2D col in GroundCheck)
        {
            if (col.transform != transform && col.tag!="CodeZone" && col.tag!="Checkpoint" && col.tag!="Ennemi")
            {
                if (col.GetComponent<BoxCollider2D>() == null)
                {
                    Grounded = true;
                    break;
                }
                else if (!col.GetComponent<BoxCollider2D>().isTrigger)
                {
                    Grounded = true;
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * positionFeet, rayFeet);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
void Flip()
    {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
        facingRight = !facingRight;
    }
}
