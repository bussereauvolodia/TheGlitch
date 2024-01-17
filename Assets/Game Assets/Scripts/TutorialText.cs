using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    private TextMeshPro textObject;
    public string text;
    private Collider2D[] PlayerCheck;
    public bool PlayerFound = false;
    private int i = 0;

    void Start()
    {
        textObject= GetComponent<TextMeshPro>();
        text="> " + textObject.text;
        textObject.text = "";
    }

    void Update()
    {
        if (!PlayerFound)
        {
            PlayerCheck = Physics2D.OverlapBoxAll(transform.position, new Vector3(12, 10), 0);
            foreach (Collider2D col in PlayerCheck)
            {
                if (col.tag == "Player")
                {
                    PlayerFound = true;
                    StartCoroutine(WriteText());
                }
            }
        }
    }

    private IEnumerator WriteText()
    {
        i = 0;
        while (text.Length > i)
        {
            textObject.text = text.Substring(0,i+1) ;
            i += 1;
            yield return new WaitForSeconds(1.65f/text.Length);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(12, 10));
    }
}


