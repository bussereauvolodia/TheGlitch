using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public GameObject me;
    private SpriteRenderer sprites;
    private float scale;
    public Sprite Selected;
    public Sprite Normal;
    private Vector3 positionBase;
    public float scaleBase = 1f;
    private Rigidbody2D rb;
    public PhysicsMaterial2D phys;

    // Start is called before the first frame update
    void Start()
    {
        sprites = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        scale = scaleBase;
        rb.mass= scale*2;
        phys.friction= scale*1.5f;
        positionBase = transform.localPosition;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if (Selection.SelectedObj == me)
            {
                scale*=2;
                if (scale == 4) { scale = 0.5f;}
                transform.localScale= new Vector3(scale,scale,scale);
                rb.mass= scale*5;
                rb.angularDrag = scale*scale*4;
                phys.friction= scale*1.5f;
                if (transform.parent != null)
                {
                    transform.localScale /= transform.parent.localScale.x;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if (Selection.SelectedObj == me)
            {
                Respawn();
            }
        }

        if (Selection.SelectedObj == me && sprites.sprite != Selected)
        {
            sprites.sprite = Selected;
        }
        else if (Selection.SelectedObj != me && sprites.sprite == Selected)
        {
            sprites.sprite = Normal;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Selection.SelectedObj = me;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Death")
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = positionBase;
        transform.rotation = new Quaternion();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        scale = scaleBase;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
