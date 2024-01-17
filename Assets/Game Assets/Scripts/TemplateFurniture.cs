using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TemplateFurniture : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Vector3 mousePos;
    public int id;
    public Inventaire inventaireScript;
    public Shop shopScript;
    private Collider2D[] GroundCheck,SpaceCheck;
    private bool placementOk;
    public GameObject furniturePrefab;
    private Vector2 scale;
    private float DontCountCheck;
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }
    void Update()
    {
        placementOk = false;
        sprite.color = new Color(255, 0, 0, 230);
        if (sprite.enabled)
        {
            mousePos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3((mousePos.x*10).ConvertTo<int>()/10f, (mousePos.y*2).ConvertTo<int>()/2f+1, 0);

            //Placement
            
            GroundCheck = Physics2D.OverlapBoxAll(new Vector3(transform.position.x, transform.position.y - scale.y / 2), new Vector3(scale.x/2f, 0.5f), 0);
            foreach (Collider2D col in GroundCheck)
            {
                if (shopScript.shopList[id].placement == "OnGround" && col.gameObject.layer == 3)
                {
                    placementOk = true;
                }
            }
            if (shopScript.shopList[id].placement == "floor" && transform.position.y == 3)
            {
                placementOk = true;
            }
            else if (shopScript.shopList[id].placement == "wall")
            {
                placementOk = true;
            }
            SpaceCheck = Physics2D.OverlapBoxAll(transform.position, new Vector3(scale.x / 1.1f, scale.y / 1.1f), 0);
            DontCountCheck = 0;
            foreach (Collider2D col in SpaceCheck)
            {
                if (col.tag == "Player" || col.GetComponent<StoreButton>() != null)
                {
                    DontCountCheck+=1;
                }
            }
            if (SpaceCheck.Length-DontCountCheck != 0)
            {
                placementOk = false;
            }
            if (placementOk)
            {
                sprite.color = new Color(255, 255, 255, 230);
            }

            //Pose ou Annulation
            if (Input.GetMouseButtonDown(0) && placementOk)
            {
                var furniture = Instantiate(furniturePrefab, transform.position, transform.rotation);
                furniture.gameObject.GetComponent<SpriteRenderer>().sprite = shopScript.shopList[id].sprite;
                furniture.gameObject.GetComponent<FurnitureScript>().inventaireScript = inventaireScript;
                furniture.gameObject.GetComponent<FurnitureScript>().shopScript = shopScript;
                furniture.gameObject.GetComponent<FurnitureScript>().id = id;
                sprite.enabled= false;
                if (shopScript.shopList[id].layer == "ground")
                {
                    furniture.gameObject.layer = 3;
                    furniture.gameObject.GetComponent<BoxCollider2D>().size = scale;

                }
                else
                {
                    furniture.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    furniture.gameObject.GetComponent<PlatformEffector2D>().enabled = false;
                }
                StaticVar.furnitures.Add(new Furniture(id, transform.position));
            }
            else if (Input.GetMouseButtonDown(0))
            {
                sprite.enabled = false;
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
            }
        }
    }

    public void ReplaceFurnitures()
    {
        foreach (Furniture furnitureData in StaticVar.furnitures)
        {
            var furniture = Instantiate(furniturePrefab, furnitureData.pos, transform.rotation);
            furniture.gameObject.GetComponent<SpriteRenderer>().sprite = shopScript.shopList[furnitureData.id].sprite;
            furniture.gameObject.GetComponent<FurnitureScript>().inventaireScript = inventaireScript;
            scale = shopScript.shopList[furnitureData.id].sprite.rect.size / shopScript.shopList[furnitureData.id].sprite.pixelsPerUnit;
            furniture.gameObject.GetComponent<BoxCollider2D>().size = scale;
            if (shopScript.shopList[furnitureData.id].layer == "ground")
            {
                furniture.gameObject.layer = 3;
            }
            else
            {
                furniture.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                furniture.gameObject.GetComponent<PlatformEffector2D>().enabled = false;
            }
        }
    }
    public void PlaceFurniture(int _id)
    {
        id = _id;
        sprite.enabled = true;
        sprite.sprite = shopScript.shopList[id].sprite;
        scale = sprite.sprite.rect.size / sprite.sprite.pixelsPerUnit;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x,transform.position.y-scale.y/2) , new Vector3(scale.x/2f, 0.5f));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(scale.x/1.1f, scale.y/1.1f));
    }
}
