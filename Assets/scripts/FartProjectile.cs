using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    // How many seconds the fart particle will exist
    public float life;
    // Time before the fart particle begins to fade
    public float lifeTransition;
    // How fast it fades during the fast fade transition
    public float FastLifeFade;
    // Divider before the fast fading starts
    public float dividor = 2f;
    // The range within which the particle can randomly spawn
    public float extraRange;
    public static float Damage = 1f;

    [HideInInspector]
    public float BonusX;
    [HideInInspector]
    public float BonusY;

    [HideInInspector]
    public float[] RandomColorRange;

    public float MinProjectileSpeed;
    public float MaxProjectileSpeed;
    public float WallWait;
    private float timer; 
    private float Uptimer; 
    private float ExtraX; 
    private float ExtraY;

    float direction = 1;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //print(RandomColorRange);
        // Give the fart particle a random shade of green
        sr.color = Random.ColorHSV(RandomColorRange[0], RandomColorRange[1], RandomColorRange[2], RandomColorRange[3], RandomColorRange[4], RandomColorRange[5]);
        // Start the lifetime timer of the particle
        timer = life;
        // Choose the random location to spawn
        ExtraX = Random.Range(-extraRange, extraRange);
        ExtraY =  Random.Range(-extraRange, extraRange);

    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Uptimer += Time.deltaTime;
        if(timer <= 0)
        {
            // Lifetime of particle is over; destroy it
            Destroy(gameObject);
        }
        if(timer <= life/dividor)
        {
            // The fast fading has started, increase to fast fade speed
            lifeTransition = FastLifeFade;
        }
        // Make the fart particle more transparent over time
        sr.color = Color.Lerp(sr.color, Color.clear, Time.deltaTime * lifeTransition);
        // Random particle velocity
        rb.velocity = new Vector2(BonusX + ExtraX, BonusY + ExtraY) * direction * Random.Range(MinProjectileSpeed, MaxProjectileSpeed) * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Wall") && Uptimer >= WallWait)
        {
            direction = -0.2f; 
        }
    }
}
