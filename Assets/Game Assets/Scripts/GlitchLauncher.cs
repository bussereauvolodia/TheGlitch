using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GlitchLauncher : MonoBehaviour
{
    public GameObject glitchRocketPrefab;
    public IEnumerator Launch()
    {
        yield return new WaitForSeconds(1.5f);
        var rocket = Instantiate(glitchRocketPrefab, transform.position, transform.rotation);
        rocket.GetComponent<Rigidbody2D>().velocity = Vector3.up*10f; ;
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

}
