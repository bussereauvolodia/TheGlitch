using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Animator animator;
    public LayerMask Ground;
    private EnnemiLife Ennemi;
    private BossFight Boss;
    private float damage=5f;
    void Awake()
    {
        animator= GetComponent<Animator>();
        animator.SetBool("IsOne",(Random.Range(0, 2) == 1));
        transform.rotation = new Quaternion (0,0,0,0);
        Destroy(gameObject,2f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.tag=="Furniture")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Ennemi")
        {
            Ennemi = collision.gameObject.GetComponent<EnnemiLife>();
            Ennemi.life= Ennemi.life-damage ;
            if (Ennemi.life<=0)
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Boss")
        {
            Boss= collision.gameObject.GetComponent<BossFight>();
            if (Boss.BossState == "" || Boss.BossState == "Attack")
            {
                Boss.health-= damage ;
            }
            Destroy(gameObject);
        }
    }
}
