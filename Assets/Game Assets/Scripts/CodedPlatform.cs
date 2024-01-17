using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodedPlatform : MonoBehaviour
{
    public GameObject CodeZoneParent;
    private Collider2D[] CollisionBox;
    void Awake()
    {
        Destroy(gameObject, 2);
        CollisionBox = Physics2D.OverlapBoxAll(transform.position, new Vector2(2,0.8f),0);
        foreach(Collider2D col in CollisionBox)
        {
            if (col.transform != transform && col.tag!="Player" && col.tag!="CodeZone")
            {
                Destroy(gameObject);
            }
        }

    }
    private void OnDestroy()
    {
        CodeZoneParent.GetComponent<ZoneCode>().PlatformCount--;
    }
}
