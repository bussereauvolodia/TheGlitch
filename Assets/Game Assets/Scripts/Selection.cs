using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public static GameObject SelectedObj;

    private GameObject PreviousSelect;
    private Transform Select_transform;

    public float test;

    private void Start()
    {
        PreviousSelect = null;
        SelectedObj = null;
        Select_transform= GetComponent<Transform>();

}

    private void FixedUpdate()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1)){
            if (PreviousSelect == SelectedObj)
            {
                SelectedObj = null;
                PreviousSelect = null;
                Select_transform.position = new Vector3(0, -8, 0);
                transform.SetParent(null);
            }
            else{
                transform.SetParent(null);
                PreviousSelect = SelectedObj;
                Select_transform.position = new Vector3(SelectedObj.transform.position.x, SelectedObj.transform.position.y, SelectedObj.transform.position.z-1);
                Select_transform.rotation = SelectedObj.transform.rotation;
                Select_transform.localScale = SelectedObj.transform.localScale*SelectedObj.GetComponent<SpriteRenderer>().sprite.rect.size / SelectedObj.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
                transform.SetParent(SelectedObj.transform);
                if (SelectedObj.GetComponent<BoxScript>() != null && SelectedObj.transform.parent != null)
                {
                    Select_transform.localScale = Vector3.one;
                }
            }
        }
    }
}
