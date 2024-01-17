using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    private Vector3 posBase;
    public string action;

    void Start()
    {
        posBase= transform.position;
        StartCoroutine(GlitchEffect());
    }

    IEnumerator RandomGlitch()
    {
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(GlitchEffect());
    }

    IEnumerator GlitchEffect()
    {
        if (action == "Right")
        {
            for (int i = 0; i < 10; i++)
            {
                transform.position = new Vector2(transform.position.x + 5, posBase.y + Random.Range(-10,10));
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 10; i++)
            {
                transform.position = new Vector2(transform.position.x - 5, posBase.y + Random.Range(-10, 10));
                yield return new WaitForSeconds(0.05f);
            }
        }
        else if (action == "Left")
        {
            for (int i = 0; i < 10; i++)
            {
                transform.position = new Vector2(transform.position.x - 5, posBase.y + Random.Range(-10, 10));
                yield return new WaitForSeconds(0.05f);
            }
            for (int i = 0; i < 10; i++)
            {
                transform.position = new Vector2(transform.position.x + 5, posBase.y + Random.Range(-10, 10));
                yield return new WaitForSeconds(0.05f);
            }
        }
        else if (action == "Middle")
        {
            for (int i = 0; i < 20; i++)
            {
                transform.position = new Vector2(posBase.x - Random.Range(-5, 5) , posBase.y + Random.Range(-10, 10));
                yield return new WaitForSeconds(0.05f);
            }
        }
        transform.position = posBase;
        StartCoroutine(RandomGlitch());
    }
}
