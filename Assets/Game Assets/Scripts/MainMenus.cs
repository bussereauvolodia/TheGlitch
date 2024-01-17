using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainMenus : MonoBehaviour
{
    public GameObject shopMenu;
    public GameObject inventaireMenu;
    public GameObject levelMenu;
    private Canvas menu;
    void Start()
    {
        shopMenu.SetActive(true);
        inventaireMenu.SetActive(false);
        menu = GetComponent<Canvas>();
        menu.enabled= false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            menu.enabled= !menu.enabled;
            if (Time.timeScale==0f) { Time.timeScale = 1f; }
            else { Time.timeScale = 0f; }
        }

        //Cheat Code
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StaticVar.FragmentsCount += 100;
            StaticVar.FragmentsDisplay.UpdateUI();
        }
    }

    public void openShop()
    {
        shopMenu.SetActive(true);
        inventaireMenu.SetActive(false);
    }
    public void openInventaire()
    {
        shopMenu.SetActive(false);
        inventaireMenu.SetActive(true);
    }
}
