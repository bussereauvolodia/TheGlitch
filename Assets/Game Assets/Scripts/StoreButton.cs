using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    private void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        transform.parent.gameObject.GetComponent<FurnitureScript>().MouseOver = true;

        if (Input.GetMouseButtonDown(0))
        {
            transform.parent.gameObject.GetComponent<FurnitureScript>().Store();
        }
    }

    private void OnMouseExit()
    {
        transform.parent.gameObject.GetComponent<FurnitureScript>().MouseOver = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
