using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnnemiLife : MonoBehaviour
{
    public float life = 20f;
    public GameObject FragmentPrefab; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        var fragment = Instantiate(FragmentPrefab, transform.position, transform.rotation);
    }
}
