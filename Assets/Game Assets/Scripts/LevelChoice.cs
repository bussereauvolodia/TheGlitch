using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelChoice : MonoBehaviour
{
    public int selectLevel;
    public Button level2Button;
    public GameObject level2Text;
    private Collider2D[] PlayerCheck;
    public GameObject Levelmenu;
    public Canvas menu;
    public GameObject textInteraction;
    public Sprite[] levelSprites;
    public SpriteRenderer levelSprite;
    private void Start()
    {
        levelSprite.sprite = levelSprites[0];
        selectLevel= 1;
        Levelmenu.SetActive(false);
        if (StaticVar.level == 0)
        {
            StaticVar.level = 1;
            level2Text.GetComponent<TextMeshProUGUI>().color = new Color32(0,255,0,50);
        }
        else
        {
            if (StaticVar.level >= 2)
            {
                level2Text.GetComponent<TextMeshProUGUI>().color = new Color32(0, 255, 0,255);
            }
            else
            {
                level2Text.GetComponent<TextMeshProUGUI>().color = new Color32(0,255,0,50);
            }
        }
    }

    private void Update()
    {
        levelSprite.sprite = levelSprites[selectLevel-1];
        textInteraction.SetActive(false);
        PlayerCheck = Physics2D.OverlapBoxAll(transform.position, new Vector3(3, 3), 0);
        foreach (Collider2D col in PlayerCheck)
        {
            if (col.tag == "Player")
            {
                textInteraction.SetActive(true);
                if (Input.GetKeyDown("f") && !menu.enabled)
                {
                    Levelmenu.SetActive(!Levelmenu.activeSelf);
                    if (Time.timeScale == 0f) { Time.timeScale = 1f; }
                    else { Time.timeScale = 0f; }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Levelmenu.activeSelf)
            {
                Levelmenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void SelectLevel(int lvl)
    {
        if (StaticVar.level >= lvl)
        {
            selectLevel = lvl;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(3, 3));
    }
}
