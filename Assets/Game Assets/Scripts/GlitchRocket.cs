using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchRocket : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject,3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerLife>().takeDamage(2, (collision.transform.position.x - transform.position.x)*6);
        }
    }
}
