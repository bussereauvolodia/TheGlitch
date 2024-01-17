using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture
{
    public int id;
    public Vector3 pos;

    public Furniture(int _id, Vector3 _pos)
    {
        id = _id;
        pos = _pos;
    }
}
public class StaticVar : MonoBehaviour
{
    public static CodeFragmentCount FragmentsDisplay;
    public static int FragmentsCount;
    public static int[][] inventaire;
    public static List<Furniture> furnitures = new List<Furniture>();
    public static int level;
}
