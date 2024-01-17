using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeFragmentScript : MonoBehaviour
{
    private CodeFragmentCount FragmentCount;
    public int Fragmentvalue;
    private void Start()
    {
        FragmentCount = StaticVar.FragmentsDisplay;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FragmentCount.AddFragment(Fragmentvalue);
            Destroy(gameObject);
        }
    }

}
