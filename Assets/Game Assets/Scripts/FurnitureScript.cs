using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FurnitureScript : MonoBehaviour
{
    private Collider2D[] PlayerCheck;
    private Collider2D[] OnTopCheck;
    private bool PlayerFound;
    public GameObject StoreButton;
    private Sprite sprite;
    public bool MouseOver;
    public Inventaire inventaireScript;
    public Shop shopScript;
    private Vector3 scale;
    public int id;

    void Start()
    {
        scale = GetComponent<BoxCollider2D>().size;
        sprite = GetComponent<SpriteRenderer>().sprite;
        StoreButton.transform.position = new Vector3(transform.position.x + sprite.rect.size.x / (sprite.pixelsPerUnit*2), transform.position.y + sprite.rect.size.y / (sprite.pixelsPerUnit * 2),transform.position.z-3) ;
        StoreButton.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        PlayerFound= false;
        PlayerCheck = Physics2D.OverlapBoxAll(transform.position, transform.localScale*1.2f, 0);
        foreach (Collider2D col in PlayerCheck)
        {
            if (col.tag == "Player")
            {
                PlayerFound= true;
            }
        }
        StoreButton.GetComponent<SpriteRenderer>().enabled = MouseOver;
    }

    private void OnMouseOver()
    {
        MouseOver = true;
    }

    private void OnMouseExit()
    {
        MouseOver = false;
    }
    public void Store()
    {
        OnTopCheck = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + transform.localScale.y / 2), new Vector2(transform.localScale.x, 0.5f),0) ;
        foreach (Collider2D col in OnTopCheck)
        {
            if (col.transform != transform && col.GetComponent<FurnitureScript>()!=null)
            {
                if (shopScript.shopList[col.GetComponent<FurnitureScript>().id].placement == "OnGround")
                {
                    col.GetComponent<FurnitureScript>().Store();
                }
            }
        }
        for (int i = 0; i < inventaireScript.inventaire.Length; i++)
        {
            if (inventaireScript.inventaire[i] != null)
            {
                if (inventaireScript.inventaire[i][0] == id)
                {
                    inventaireScript.inventaire[i][1] += 1;
                    break;
                }
            }
            else
            {
                inventaireScript.inventaire[i] = new int[2] { id, 1 };
                break;
            }
        }
        inventaireScript.UpdateInventaire();
        foreach(Furniture furniture in StaticVar.furnitures)
        {
            if (furniture.id==id && furniture.pos==transform.position)
            {
                StaticVar.furnitures.Remove(furniture);
                break;
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
       Gizmos.color = Color.yellow;
       Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + scale.y / 2), new Vector2(scale.x, 0.5f)) ;
    }
}
