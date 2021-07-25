using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Health")]
    public float StartingHealth;
    public float DamageInvisDuration;
    public float RegenRate;
    [Space]
    [Header("Speed")]
    public float Speed;
    public float lerpSpeed;
    [Space]
    [Header("Attack/Reload")]
    public float FartTotal;
    public float FartReload;
    public float ReloadSpeed;
    public float LastDeterminer;
    public float BonusMovement;
    public Transform FartSpawn;
    [HideInInspector]
    public float FartAmount;
    [HideInInspector]
    public float refreshTimer;
    [Space]
    [Header("EnemyAttack")]
    public float ZombieEnemy = 20f;
    public float Projectile = 5f;
    public float ThiccZombie = 25f;
    public GameObject ProjectileParticle;
    [Space]
    [Header("Audio")]
    public GameObject HitAudio;

    [Space]
    [Header("Progression")]
    public static int ProgressionLevel = 1;
    public static int TotalEnemies;
    public static int AmountKilled;
    public GameObject TransitionScreen;
    public GameObject Music;

    float timer;
    float InputX;
    float InputY;
    float LastX;
    float LastY;
    float PlayerHealth;
    [HideInInspector]
    public float BonusX;
    [HideInInspector]
    public float BonusY;

    [HideInInspector]
    public float[] RandomColorRange;
    public float FartDamage;

    bool loaded; 

    int directionX;
    int directionY;

    public GameObject FartBullet;
    public CameraScript cam;

    public Text LevelText;
    public Slider FartSlider;
    public Slider HealthBar;

    Vector2 MovementDirection;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioSource FartSound;

    private void Awake()
    {
        loaded = false;
        TotalEnemies = 0;
        AmountKilled = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = StartingHealth;
        RandomColorRange = new float[] {0.2f, 0.3f, 1f, 1f, 0.5f, 1f, 0.8f, 1f};
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        FartSound = GetComponent<AudioSource> ();
        FartAmount = FartTotal;
        LastX = 1;
        LastY = 0;
        timer = 0x3f3f3f3f;
        if(ProgressionLevel == 1)
        {
            Instantiate(Music, Music.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(AmountKilled >= TotalEnemies && !loaded)
        {
            loaded = true;
            Instantiate(TransitionScreen, TransitionScreen.transform.position, Quaternion.identity);
            Invoke("NextLevel", 1f);
        }
        Vector3 diff = new Vector3(LastX, LastY, 0);
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        float ZFartDirection = rot_z - 90;

        //LevelText:
        LevelText.text = "Level: " + ProgressionLevel;


        //Healthbar: 
        HealthBar.value = PlayerHealth / StartingHealth;
        PlayerHealth = Mathf.Clamp(PlayerHealth, -100, StartingHealth);
        if (PlayerHealth < StartingHealth)
        {
            PlayerHealth += RegenRate * Time.deltaTime * 0.33f;
        }
        if(PlayerHealth <= 0 && !loaded)
        {
            loaded = true;
            Instantiate(TransitionScreen, TransitionScreen.transform.position, Quaternion.identity);
            Invoke("GameOver", 1f);
        }

        //Color: 
        if (timer > DamageInvisDuration)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = new Color32(255,255,255,80);
        }
        //Timers: 
        refreshTimer -= Time.deltaTime;
        timer += Time.deltaTime;

        if (Input.GetButton("Jump"))
        {
            FartAmount -= Time.deltaTime;
            refreshTimer = FartReload;
            if (FartAmount > 0)
            {
                if(!FartSound.isPlaying)
                {
                    FartSound.pitch = Random.Range(0.1f, 2f);
                    FartSound.Play();
                }
                GameObject Bullet = Instantiate(FartBullet, FartSpawn.position, Quaternion.identity);
                FartProjectile fp;
                fp = Bullet.GetComponent<FartProjectile>();
                fp.BonusX = this.BonusX;
                fp.BonusY = this.BonusY;
                fp.RandomColorRange = this.RandomColorRange;
                Bullet.transform.rotation = Quaternion.Euler(0,0, -ZFartDirection);

                BonusX = LastX;
                BonusY = LastY;
            }
            else
            {
                BonusX = 0;
                BonusY = 0;
            }
        }
        else
        {
            BonusX = 0;
            BonusY = 0;
        }
        if (refreshTimer <= 0)
        {

            FartAmount = Mathf.Lerp(FartAmount, FartTotal, ReloadSpeed * Time.deltaTime);
            FartAmount += 0.2f * Time.deltaTime;

        }
        FartAmount = Mathf.Clamp(FartAmount,0, FartTotal);

        FartSlider.value = FartAmount / FartTotal;

        if (Mathf.Abs(InputX) > 0.2f || Mathf.Abs(InputY) > 0.2f) 
        {
            LastX = Mathf.Lerp(LastX, InputX, LastDeterminer * Time.deltaTime);
            LastY = Mathf.Lerp(LastY, InputY, LastDeterminer * Time.deltaTime);
        }
        if(InputX > 0)
        {
            sr.flipX = false;
        }
        else if (InputX < 0)
        {
            sr.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        InputX = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * Speed; 
        InputY = Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime * Speed;
        MovementDirection = Vector2.ClampMagnitude(Vector2.Lerp(MovementDirection, new Vector2(InputX, InputY), lerpSpeed * Time.deltaTime), 350f);
        rb.velocity = MovementDirection + (new Vector2(BonusX, BonusY) * BonusMovement * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag("EnemyBullet"))
        {
            TakeDamage(Projectile);
            Destroy(col);
            Instantiate(ProjectileParticle, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag("ZombieEnemy"))
        {
            TakeDamage(ZombieEnemy);
        }
        if (col.CompareTag("ThiccEnemy"))
        {
            TakeDamage(ThiccZombie);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag("ZombieEnemy"))
        {
            TakeDamage(ZombieEnemy);
        }
        if (col.CompareTag("ThiccEnemy"))
        {
            TakeDamage(ThiccZombie);
        }
    }

    void TakeDamage(float Amount)
    {
        if (timer > DamageInvisDuration)
        {
            PlayerHealth -= Amount;
            Instantiate(HitAudio, transform.position, transform.rotation);
            StartCoroutine(cam.Shake(0.2f,0.15f));
            timer = 0;
        }
    }    

    void GameOver()
    {
        Destroy(GameObject.FindGameObjectWithTag("MusicPlayer"));
        SceneManager.LoadScene("DefeatScene");
    }

    void NextLevel()
    {
        ProgressionLevel += 1;
        SceneManager.LoadScene("level");
    }
}
