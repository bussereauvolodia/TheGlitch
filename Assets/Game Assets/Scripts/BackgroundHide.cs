using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHide : MonoBehaviour
{
    public float power;
    private float clock;
    private float cooldown;
    private SpriteRenderer midSprite;
    public SpriteRenderer sideSprite1, sideSprite2;
    void Start()
    {
        clock = Time.time;
        cooldown = power + Random.Range(1, 10);
        midSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (clock + cooldown < Time.time)
        {
            clock = Time.time;
            cooldown = power + Random.Range(1, 10);

            midSprite.enabled = false;
            sideSprite1.enabled = false;
            sideSprite2.enabled = false;

            
        }
        if (!midSprite.enabled && clock+0.3<Time.time)
        {
            midSprite.enabled = true;
            sideSprite1.enabled = true;
            sideSprite2.enabled = true;
        }
    }
}
