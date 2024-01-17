using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlatformMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform Transform;
    public float difX=5,difY=5;
    public float OffsetX,OffsetY;
    private float posXMiddle,posYMiddle;
    public float speed;
    private float dir=1;
    public bool MouvX;
    public GameObject me;
    private SpriteRenderer sprites;
    public Sprite Selected, Normal;
    private bool MouvXBase;
    private ConstraintSource mySource;
    // Start is called before the first frame update
    void Awake()
    {
        MouvXBase = MouvX;
        Transform = GetComponent<Transform>();
        sprites = GetComponent<SpriteRenderer>();
        posXMiddle= Transform.position.x;
        posYMiddle= Transform.position.y;
        Transform.position = new Vector3(Transform.position.x+OffsetX,Transform.position.y+OffsetY,Transform.position.z);
        mySource.sourceTransform= transform;
        mySource.weight= 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (MouvX)
        {
            Transform.position = Vector3.MoveTowards(Transform.position, new Vector3(posXMiddle+(difX*dir),Transform.position.y,Transform.position.z),speed*Time.deltaTime);

            if (Vector2.Distance(Transform.position,new Vector2(posXMiddle+(difX*dir),Transform.position.y))<0.1)
            {
                dir*=-1;
            }
        } 
        else{
            Transform.position = Vector3.MoveTowards(Transform.position, new Vector3(Transform.position.x,posYMiddle+(difY*dir), Transform.position.z),speed*Time.deltaTime);

            if (Vector2.Distance(Transform.position,new Vector2(Transform.position.x,posYMiddle+(difY*dir)))<0.1)
            {
                dir*=-1;
            }
        }

        if (Input.GetMouseButton(2) && Selection.SelectedObj == me)
        {

            if (Input.GetKeyDown("z"))
            {
                MouvX = false;
                dir = 1;
            }
            else if (Input.GetKeyDown("s"))
            {
                MouvX = false;
                dir = -1;
            }
            else if (Input.GetKeyDown("d"))
            {
                MouvX = true;
                dir = 1;
            }
            else if (Input.GetKeyDown("q"))
            {
                MouvX = true;
                dir = -1;

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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.position.y > Transform.position.y)
        {
            col.transform.SetParent(Transform);
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.parent == Transform && col.transform.position.y < Transform.position.y)
        {
            col.transform.SetParent(null);
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        col.transform.SetParent(null);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Selection.SelectedObj = me;
        }
    }

    public void RespawnPlatform()
    {
        Transform.position = new Vector3(posXMiddle + OffsetX, posYMiddle + OffsetY, Transform.position.z);
        MouvX = MouvXBase;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (posXMiddle == 0)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(difX * 2, difY * 2));
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(transform.position.x+OffsetX,transform.position.y+OffsetY), new Vector3(2, 0.5f));
            Gizmos.color = Color.yellow;
            if (MouvX)
            {
                Gizmos.DrawRay(new Vector3(transform.position.x + OffsetX, transform.position.y + OffsetY), new Vector3(dir, 0, 0));
            }
            else
            {
                Gizmos.DrawRay(new Vector3(transform.position.x + OffsetX, transform.position.y + OffsetY), new Vector3(0, dir, 0));
            }
        }
        else
        {
            Gizmos.DrawWireCube(new Vector3(posXMiddle, posYMiddle), new Vector3(difX * 2, difY * 2));
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(posXMiddle + OffsetX, posYMiddle + OffsetY), new Vector3(2, 0.5f));
            Gizmos.color = Color.yellow;
            if (MouvX)
            {
                Gizmos.DrawRay(new Vector3(posXMiddle + OffsetX, posYMiddle + OffsetY), new Vector3(dir,0,0));
            }
            else
            {
                Gizmos.DrawRay(new Vector3(posXMiddle + OffsetX, posYMiddle + OffsetY), new Vector3(0, dir, 0));
            }
        }

    }

}

