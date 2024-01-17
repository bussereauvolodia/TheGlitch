using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortal : MonoBehaviour
{
    public GameObject menuVictoire;
    public CodeFragmentCount countFragment;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Time.timeScale = 0f;
            menuVictoire.SetActive(true);
            StaticVar.FragmentsCount += countFragment.FragmentsCollected;
            if (StaticVar.level <= SceneManager.GetActiveScene().buildIndex)
            {
                StaticVar.level = SceneManager.GetActiveScene().buildIndex;
            }
        }
    }

    void Start()
    {
        menuVictoire.SetActive(false);
    }
}
