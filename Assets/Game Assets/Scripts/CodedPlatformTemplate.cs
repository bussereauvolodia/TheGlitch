using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodedPlatformTemplate : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool playerInZone = false;
    private Collider2D[] CollisionBox;
    public Sprite NormalSprite;
    public Sprite IncorrectSprite;
    void Start()
    {
       spriteRenderer = GetComponent<SpriteRenderer>(); 
       spriteRenderer.enabled = false;
    }
    void Update()
    {
        spriteRenderer.enabled = playerInZone;
        spriteRenderer.sprite = NormalSprite;
        CollisionBox = Physics2D.OverlapBoxAll(transform.position,new Vector2(2,0.8f),0);
        foreach (Collider2D col in CollisionBox)
        {
            if (col.transform != transform && col.tag != "Player" && col.tag != "CodeZone")
            {
                spriteRenderer.sprite = IncorrectSprite;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(2,0.8f));
    }
}
