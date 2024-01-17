using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    public int[][] inventaire;
    public Shop shopScript;
    private GameObject[] inventaireSlots = new GameObject[27];
    private int index;
    public TemplateFurniture templateFurniture;
    public GameObject menu;
    void Start()
    {
        index = 0;
        if (shopScript.shopList.Length>=27) { inventaire = new int[shopScript.shopList.Length][]; }
        else { inventaire = new int[27][]; }
        if (StaticVar.inventaire != null)
        {
            inventaire = StaticVar.inventaire;
        }
        for (int i = 0; i < 27; i++)
        {
            inventaireSlots[i] = transform.GetChild(i).gameObject;
        }
    }

    private GameObject title, display, count;

    public void UpdateInventaire()
    {
        for (int i = 0; i < inventaireSlots.Length; i++)
        {
            title = inventaireSlots[i].transform.GetChild(0).gameObject;
            display = inventaireSlots[i].transform.GetChild(1).gameObject;
            count = inventaireSlots[i].transform.GetChild(2).gameObject;
            if (inventaire[index * 9 + i] != null)
            {
                if (inventaire[index * 9 + i][1] == 0)
                {
                    arrangeInventaire(index * 9 + i);
                }
            }
            if (inventaire[index * 9 + i] != null)
            {
                title.GetComponent<TextMeshProUGUI>().text = shopScript.shopList[inventaire[index * 9 + i][0]].name;
                if (35f / title.GetComponent<TextMeshProUGUI>().text.Length < 7)
                {
                    title.GetComponent<TextMeshProUGUI>().fontSize = 68f / title.GetComponent<TextMeshProUGUI>().text.Length;
                }
                else
                {
                    title.GetComponent<TextMeshProUGUI>().fontSize = 7;
                }
                display.GetComponent<Image>().sprite = shopScript.shopList[inventaire[index * 9 + i][0]].sprite;
                display.GetComponent<Image>().enabled = true;
                display.transform.localScale = new Vector2(0.35005f, 0.37501f) * (shopScript.shopList[inventaire[index * 9 + i][0]].sprite.rect.size / 32f).normalized;
                count.GetComponent<TextMeshProUGUI>().text = inventaire[index * 9 + i][1].ToString();
            }
            else
            {
                title.GetComponent<TextMeshProUGUI>().text = "";
                display.GetComponent<Image>().enabled = false;
                count.GetComponent<TextMeshProUGUI>().text = "";
            }
        }
        StaticVar.inventaire= inventaire;
    }

    private void arrangeInventaire(int idBegin)
    {
        for (int i = 0; i < inventaire.Length-idBegin-1; i++)
        {
            inventaire[idBegin + i] = inventaire[idBegin + i + 1];
        }
    }
    public void PlaceFurniture(int id)
    {
        if (inventaire[index * 9 + id] != null && menu.GetComponent<Canvas>().enabled)
        {
            inventaire[index * 9 + id][1]--;
            templateFurniture.PlaceFurniture(inventaire[index * 9 + id][0]);
            UpdateInventaire();
            menu.GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1f;
        }
    }
}
