using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnnemiIA : MonoBehaviour
{
    public float visionRay = 10;
    public float speed = 2;
    private Collider2D[] PlayerCheck;
    private Rigidbody2D rb;

    private Animator animator;
    private bool facingRight = true;

    private Vector3 posPlayer;
    private bool PlayerFound;

    public float damage = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
    void Update()
    {
        if (rb.velocity.x > 0.1)
        {
            if (!facingRight) { Flip(); }
        }
        else if (rb.velocity.x < -0.1)
        {
            if (facingRight) { Flip(); }
        }

        PlayerFound = false;
        PlayerCheck = Physics2D.OverlapBoxAll(transform.position, new Vector3(visionRay, 2), 0);
        foreach (Collider2D col in PlayerCheck)
        {
            if (col.tag == "Player")
            {
                PlayerFound=true;
                posPlayer = col.GetComponent<Transform>().position;
                if (posPlayer.x < transform.position.x)
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
            }
        }
        if (!PlayerFound && rb.velocity.y!=0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerLife>().takeDamage(damage, (collision.transform.position.x- transform.position.x)*8);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(visionRay, 2));
    }
    void Flip()
    {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
        facingRight = !facingRight;
    }
}
