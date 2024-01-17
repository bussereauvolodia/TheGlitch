using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossFight : MonoBehaviour
{
    private Collider2D[] PlayerCheck;
    public float health;
    public string BossState;
    private Vector3 positionBase;
    private Rigidbody2D rb;
    public Transform WallR, WallL;
    private bool facingRight = true;
    private float xTarget;
    private Animator anim;
    public GlitchLauncher[] glitchLaunchers;
    public GameObject Player;
    public HealthBarScript healthbar;
    public BossTrigger bossTrigger;
    public GameObject BossTilemap;
    public GameObject glitchRocketPrefab;
    public GameObject gemPrefab;
    private bool GoDown;

    public AudioSource audioManager;
    public AudioClip musicLevel2, musicBoss;
    void Start()
    {
        health = 400;
        BossState = "Wait";
        positionBase = transform.position;
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Player.GetComponent<PlayerLife>().bossScript = GetComponent<BossFight>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        anim.SetBool("Attack", BossState=="Attack");
        anim.SetBool("God", BossState=="God");
        healthbar.SetHealth(health);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            GoDown=true;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            health -=20;
        }
        if (health <= 0)
        {
            healthbar.gameObject.SetActive(false);
            bossTrigger.BossDeath();
            audioManager.clip = musicLevel2;
            audioManager.Play();
            var gem = Instantiate(gemPrefab, transform.position, transform.rotation);
            gem.GetComponent<CodeFragmentScript>().Fragmentvalue = 20;
            Destroy(gameObject);
        }
        else if (health <= 200 && (BossState == "" || BossState =="Attack") && BossTilemap.activeSelf)
        {
            StopAllCoroutines();
            anim.SetBool("Landing", false);
            StartCoroutine(StartFight(false));
        }
        if (BossState == "Wait")
        {
            audioManager.volume-=0.0005f;
            PlayerCheck = Physics2D.OverlapBoxAll(transform.position, new Vector3(3, 7), 0);
            foreach (Collider2D col in PlayerCheck)
            {
                if (col.tag == "Player")
                {
                    BossState = "Start";
                    StartCoroutine(StartFight(true));
                    audioManager.clip = musicBoss;
                    audioManager.volume = 0.3f;
                    audioManager.Play();
                }
                else if (col.gameObject.GetComponent<BulletScript>()!=null){
                    Destroy(col.gameObject);
                    BossState = "Start";
                    StartCoroutine(StartFight(true));
                    audioManager.clip = musicBoss;
                    audioManager.volume = 0.3f;
                    audioManager.Play();
                }
            }
        }
        else if (BossState == "Idle"){
            if(health<=200 && BossTilemap.activeSelf)
            {
                BossState = "God";
                StartCoroutine(God());
            }
            if (xTarget == 0 || Mathf.Abs(xTarget - transform.position.x) < 0.1)
            {
                if (xTarget==positionBase.x)
                {
                    BossState = "GroundPound";
                    xTarget = 0;
                    rb.velocity = Vector3.zero;

                }
                else if (GoDown)
                {
                    GoDown = false;
                    xTarget = positionBase.x;
                }
                else
                {
                    int r = Random.Range(0, 3);
                    if (r == 0 || xTarget==0)
                    {
                        xTarget = Random.Range(WallR.position.x - WallR.localScale.x, WallL.position.x + WallL.localScale.x);
                    }
                    else if (r == 1)
                    {
                        BossState = "ArmUp";
                        xTarget = 0;
                        rb.velocity = Vector3.zero;
                        anim.SetTrigger("ArmUp");
                    }
                    else
                    {
                        xTarget = positionBase.x;
                    }
                }
                
            }
            else
            {
                if (transform.position.x - xTarget < 0)
                {
                    rb.velocity = Vector3.right*4;
                }
                else
                {
                    rb.velocity = Vector3.left*4;
                }
            }
            if (rb.velocity.x > 0.1)
            {
                if (!facingRight) { Flip(); }
            }
            else if (rb.velocity.x < -0.1)
            {
                if (facingRight) { Flip(); }
            }
        }
        else if (BossState == "ArmUp")
        {
            float r = Random.Range(0, 3);
            for (int i = 0; i < 3; i++)
            {
                if (i!=r)
                {
                    glitchLaunchers[i].gameObject.SetActive(true);
                    StartCoroutine(glitchLaunchers[i].Launch());
                }
            }
            StartCoroutine(Wait(3,"Idle"));
        }
        else if(BossState == "GroundPound")
        {
            anim.SetBool("Landing", true);
            rb.gravityScale = 5f;
            if (Mathf.Abs(transform.position.y - positionBase.y) < 0.5)
            {
                var rocketR = Instantiate(glitchRocketPrefab, transform.position, Quaternion.Euler(0, 0, -90));
                rocketR.GetComponent<Rigidbody2D>().velocity = Vector3.right * 8f;
                var rocketL = Instantiate(glitchRocketPrefab, transform.position, Quaternion.Euler(0, 0, 90));
                rocketL.GetComponent<Rigidbody2D>().velocity = Vector3.left * 8f;
                StartCoroutine(Wait(3, "StartAttack"));
            }
        }
        else if(BossState == "StartAttack")
        {
            anim.SetBool("Landing", false);
            StartCoroutine(Attack());
            BossState = "Attack";
        }
        else if(BossState == "Attack")
        {
            if (Player.transform.position.x - transform.position.x > 0)
            {
                if (!facingRight) { Flip(); }
            }
            else
            {
                if (facingRight) { Flip(); }
            }
        }
        
    }

    private IEnumerator StartFight(bool ChangeSize)
    {
        BossState = "FlyUp";
        rb.gravityScale = 0;
        while (transform.position.y < 22)
        {
            rb.velocity = Vector3.up;
            if (ChangeSize) { transform.localScale *= 1.011f; }
            yield return new WaitForSeconds(0.1f);
        }
        rb.velocity = new Vector3(0, 0);
        xTarget= 0;
        BossState = "Idle";
        if (!healthbar.gameObject.activeSelf)
        {
            healthbar.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerLife>().takeDamage(10, (collision.transform.position.x - transform.position.x) * 16);
        }
    }

    private IEnumerator Wait(int delay, string nextState)
    {
        BossState = "";
        yield return new WaitForSeconds(delay);
        BossState= nextState;
    }
    private IEnumerator Attack()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            var rocket = Instantiate(glitchRocketPrefab, transform.position, Quaternion.Euler(0, 0, -90));
            if (facingRight) { rocket.GetComponent<Rigidbody2D>().velocity = Vector3.right * 8f; }
            else 
            { 
                rocket.GetComponent<Rigidbody2D>().velocity = Vector3.left * 8f;
                rocket.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            yield return new WaitForSeconds(0.5f);
        }
        BossState = "";
        yield return new WaitForSeconds(2f);
        BossState = "FlyUp";
        StartCoroutine(StartFight(false));
    }

    private IEnumerator God()
    {
        //-> ActivateParticles
        yield return new WaitForSeconds(2f);
        Tilemap sprite = BossTilemap.GetComponent<Tilemap>();
        while (sprite.color.a>=0) 
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a-0.01f) ;
            yield return new WaitForSeconds(0.05f);
        }
        BossTilemap.SetActive(false);
        BossState = "Idle";
    }

    public void BossReset()
    {
        GlitchRocket[] rockets = FindObjectsOfType<GlitchRocket>();
        foreach (GlitchRocket rocket in rockets)
        {
            Destroy(rocket.gameObject);
        }
        bossTrigger.BossReset();
        health = 400;
        BossState = "Wait";
        anim.SetBool("Landing", false);
        transform.position = positionBase;
        BossTilemap.SetActive(true);
        BossTilemap.GetComponent<Tilemap>().color = new Color(1,1,1,1);
        rb.velocity = Vector3.zero;
        rb.gravityScale = 1.98f;
        transform.localScale = new Vector3(1,1,1);
        facingRight = true;
        healthbar.gameObject.SetActive(false);
        StopAllCoroutines();
        foreach (GlitchLauncher glitchLauncher in glitchLaunchers)
        {
            glitchLauncher.gameObject.SetActive(false);
        }
        audioManager.clip = musicLevel2;
        audioManager.Play();
        gameObject.SetActive(false);
    }

    void Flip()
    {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
        facingRight = !facingRight;
    }
private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(3, 7));
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(new Vector3(WallR.position.x - WallR.localScale.x, transform.position.y), new Vector3(1, 1));
        Gizmos.DrawWireCube(new Vector3(WallL.position.x + WallL.localScale.x, transform.position.y), new Vector3(1, 1));
    }
}