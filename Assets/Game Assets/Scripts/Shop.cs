using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Product
{
    public int id;
    public string name;
    public Sprite sprite;
    public int price;
    public string placement;
    public string layer;

    public Product(int _id, string _name, Sprite _sprite, int _price, string _placement, string _layer) 
    {
        id = _id;
        name=_name; 
        sprite=_sprite;
        price=_price;
        placement=_placement;
        layer = _layer;
    }
}

public class Shop : MonoBehaviour
{
    public Product[] shopList = new Product[4];
    public Sprite[] spriteList;
    private int index;
    public GameObject[] contentList;
    private GameObject title,display,price;
    public Inventaire inventaireScript;
    public TemplateFurniture furnitureScript;
    void Awake()
    {
        index = 0;
        shopList[0] = new Product(0,"table", spriteList[0], 30, "floor","ground");
        shopList[1] = new Product(1,"tv", spriteList[1], 100, "OnGround","decoration");
        shopList[2] = new Product(2,"shelf", spriteList[2], 50, "wall","ground");
        shopList[3] = new Product(3,"Rplace Painting", spriteList[3], 50, "wall","decoration");
        UpdateShop(index);
        inventaireScript.UpdateInventaire();
        furnitureScript.ReplaceFurnitures();
    }

    void UpdateShop(int index)
    {
        for (int i = 0; i < contentList.Length; i++)
        {
            title = contentList[i].transform.GetChild(0).gameObject;
            display = contentList[i].transform.GetChild(3).gameObject;
            price = contentList[i].transform.GetChild(1).gameObject;
            title.GetComponent<TextMeshProUGUI>().text = shopList[index + i].name;
            display.GetComponent<Image>().sprite = shopList[index + i].sprite;
            display.transform.localScale = new Vector2(0.35005f, 0.37501f)*(shopList[index + i].sprite.rect.size/32f).normalized;
            price.GetComponent<TextMeshProUGUI>().text = shopList[index + i].price.ToString();
        }
    }

    public void ShopSelect(int diff)
    {
        Debug.Log(index);
        if (index+1+diff>0 && index+1+diff<shopList.Length-1)
        {
            index += diff;
            if (index > shopList.Length || index < 0)
            {
                index -= diff;
            }
            UpdateShop(index);
        }
    }

    public void Buy(int id)
    {
        if (StaticVar.FragmentsCount >= shopList[index + id].price)
        {
            StaticVar.FragmentsCount-= shopList[index + id].price;
            StaticVar.FragmentsDisplay.UpdateUI();
            for (int i = 0; i < inventaireScript.inventaire.Length; i++)
            {
                if (inventaireScript.inventaire[i] != null)
                {
                    if (inventaireScript.inventaire[i][0] == index + id)
                    {
                        inventaireScript.inventaire[i][1] += 1;
                        break;
                    }
                }
                else
                {
                    inventaireScript.inventaire[i] = new int[2] { index + id, 1 };
                    break;
                }
            }
            inventaireScript.UpdateInventaire();
        }
        else
        {
            Debug.Log("Not enought money");
        }
    }
}
