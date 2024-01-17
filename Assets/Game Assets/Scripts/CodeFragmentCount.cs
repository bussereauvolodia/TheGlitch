using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CodeFragmentCount : MonoBehaviour
{
    public GameObject DisplayCount;
    private TextMeshProUGUI CountFragments;
    public int FragmentsCollected;
    public CodeFragmentCount otherMe;

    private void Awake()
    {
        if (StaticVar.FragmentsDisplay != null)
        {
            otherMe=StaticVar.FragmentsDisplay;
        }
        StaticVar.FragmentsDisplay = gameObject.GetComponent<CodeFragmentCount>();
    }
    void Start()
    {
        CountFragments = DisplayCount.GetComponent<TextMeshProUGUI>();
        CountFragments.SetText("0");
        FragmentsCollected = 0;
        if (SceneManager.GetActiveScene().name=="Main Menu")
        {
            FragmentsCollected = StaticVar.FragmentsCount;
        }
        UpdateUI();

    }

    public void AddFragment(int nbFragment)
    {
        FragmentsCollected += nbFragment;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            FragmentsCollected = StaticVar.FragmentsCount;
        }
        CountFragments.SetText(FragmentsCollected.ToString());
        if (otherMe!= null)
        {
            otherMe.UpdateUI();
        }
    }
}
