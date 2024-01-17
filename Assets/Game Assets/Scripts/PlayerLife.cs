using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Vector3 positionBase;
    private Rigidbody2D rb;
    private PlayerControle controleScript;
    public PlatformMove[] platformScript;
    public BossFight bossScript;
    private Animator anim;

    private float health = 20f;
    public HealthBarScript healthbar;
    private bool invincibility;
    private SpriteRenderer sprite;
    private Coroutine knockbackCoroutine;
    void Start()
    {
        StartCoroutine(Respawn());
    }

    void Awake()
    {
        positionBase = transform.position;
        healthbar.SetHealth(health);
        platformScript = GameObject.FindObjectsOfType<PlatformMove>();
        rb = GetComponent<Rigidbody2D>();
        controleScript = GetComponent<PlayerControle>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        //Cheat code
        /*
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            if (Input.GetKeyUp(KeyCode.F1))
            {
                positionBase = new Vector3(-7.62f, 1.71f, positionBase.z);
                StartCoroutine(Respawn());
            }
            if (Input.GetKeyUp(KeyCode.F2))
            {
                positionBase = new Vector3(26.3056f, 11.44618f, positionBase.z);
                StartCoroutine(Respawn());
            }
            if (Input.GetKeyUp(KeyCode.F3))
            {
                positionBase = new Vector3(130.21f, 6.44618f, positionBase.z);
                StartCoroutine(Respawn());
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            if (Input.GetKeyUp(KeyCode.F1))
            {
                positionBase = new Vector3(-3.7f, 1f, positionBase.z);
                StartCoroutine(Respawn());
            }
            if (Input.GetKeyUp(KeyCode.F2))
            {
                positionBase = new Vector3(71f, 7f, positionBase.z);
                StartCoroutine(Respawn());
            }
            if (Input.GetKeyUp(KeyCode.F3))
            {
                positionBase = new Vector3(104f, 19f, positionBase.z);
                StartCoroutine(Respawn());
            }
            if (Input.GetKeyUp(KeyCode.F4))
            {
                positionBase = new Vector3(141f, 19f, positionBase.z);
                StartCoroutine(Respawn());
            }
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Death")
        {
            StartCoroutine(Respawn());
        }
        else if (collision.tag == "Checkpoint")
        {
            positionBase = transform.position;
        }
    }

    public IEnumerator Respawn()
    {
        if (!controleScript.GodMod)
        {
            if (knockbackCoroutine != null)
            {
                StopCoroutine(knockbackCoroutine);
                sprite.color = new Color(255, 255, 255);
                controleScript.Animating = false;
            }
            gameObject.transform.SetParent(null);
            rb.velocity = new Vector2(0, 0);
            controleScript.Controle = false;
            controleScript.enabled = false;
            anim.SetFloat("VelocityY", 0);
            anim.SetTrigger("Spawn");
            transform.position = positionBase;
            foreach (PlatformMove truc in platformScript)
            {
                truc.RespawnPlatform();
            }
            if (bossScript != null)
            {
                if (bossScript.BossState != "Wait")
                {
                    bossScript.BossReset();
                }
            }
            health = 20f;
            healthbar.SetHealth(health);
            yield return new WaitForSeconds(1);
            controleScript.enabled = true;
            invincibility = false;
        }
    }

    public void takeDamage(float damage,float knockback)
    {
        if (!invincibility)
        {
            health -= damage;
            healthbar.SetHealth(health);
            knockbackCoroutine = StartCoroutine(Knockback(knockback));
            if (health <= 0)
            {
                StartCoroutine(Respawn());
            }
        }
    }

    public IEnumerator Knockback(float knockback)
    {
        controleScript.Animating = true;
        invincibility = true;
        rb.velocity = new Vector2(knockback, rb.velocity.y+Mathf.Abs(knockback/2));
        sprite.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(0.25f);
        sprite.color = new Color(255, 255, 255);
        controleScript.Animating = false;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.25f);
            sprite.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(0.25f);
            sprite.color = new Color(255,255,255);
        }
        invincibility = false;
    }
}
