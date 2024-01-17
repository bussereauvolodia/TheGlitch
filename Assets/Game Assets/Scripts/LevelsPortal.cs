using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsPortal : MonoBehaviour
{
    public int sceneSelect;
    private Collider2D[] PlayerCheck;
    public GameObject LevelText;
    public LevelChoice levelChoiceScript;

    void Update()
    {
        PlayerCheck = Physics2D.OverlapBoxAll(transform.position, new Vector3(2.5f, 5), 0);
        foreach (Collider2D col in PlayerCheck)
        {
            if (col.tag == "Player")
            {
                SceneManager.LoadScene(levelChoiceScript.selectLevel+1);
            }
        }
        if (LevelText.GetComponent<TutorialText>().text != "> Level " + levelChoiceScript.selectLevel.ToString())
        {
            LevelText.GetComponent<TutorialText>().text = "> Level " + levelChoiceScript.selectLevel.ToString();
            LevelText.GetComponent<TextMeshPro>().text = "";
            LevelText.GetComponent<TutorialText>().PlayerFound = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(2.5f, 5));
    }
}
