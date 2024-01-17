using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Glitch : MonoBehaviour
{
    public Transform Screen;
    private Animator AnimGlitch;
    private SpriteRenderer render;
    private float Clock;
    private float CoolDown;

    void Start()
    {
        AnimGlitch = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Title Screen")
        {
            if (AnimGlitch.GetCurrentAnimatorStateInfo(0).IsName("Glitch_Empty") && !AnimGlitch.GetBool("Pos"))
            {
                if ((Time.time < Clock + CoolDown) && (render.enabled = true))
                {
                    render.enabled = false;
                }
                else if (Time.time > Clock + CoolDown)
                {
                    render.enabled = true;
                    transform.position = new Vector3(Random.Range(Screen.position.x - Screen.localScale.x * 7, Screen.position.x + Screen.localScale.x * 7), Random.Range(Screen.position.y - Screen.localScale.y * 4, Screen.position.y + Screen.localScale.y * 4), transform.position.z);
                    AnimGlitch.SetBool("Pos", true);
                }
            }
            else if (AnimGlitch.GetCurrentAnimatorStateInfo(0).IsName("Glitch_anim") && AnimGlitch.GetBool("Pos"))
            {
                AnimGlitch.SetBool("Pos", false);
                Clock = Time.time;
                CoolDown = Random.Range(1, 5);
            }
        }
        else
        {
            AnimGlitch.SetBool("Pos", true);
        }
    }
}
