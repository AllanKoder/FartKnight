using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    public float speed;
    public float BonusSpeed;
    public float FastDistance;
    public float BackAwayRange;
    public float RangeIncrease;
    public Vector2 RandomOffset;
    private float multiplier = 1f; 
    Vector3 SetAddon;
    Vector3 addon;
    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetAddon = new Vector3(Random.Range(-RandomOffset.x, RandomOffset.x), Random.Range(-RandomOffset.y, RandomOffset.y), 0);

    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x - transform.position.x > 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        addon = SetAddon * Vector3.Distance(player.position, transform.position) * RangeIncrease;
        if (Vector3.Distance(transform.position, player.position) < BackAwayRange - 1)
        {
            multiplier = -1f;
        }
        else if (Vector3.Distance(transform.position, player.position) > BackAwayRange + 1)
        {
            multiplier = 1f;
        }
        else
        {
            multiplier = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = multiplier * Vector3.ClampMagnitude(addon + player.position - transform.position, 0.1f) * speed * Time.fixedDeltaTime;
        if (Vector3.Distance(transform.position, player.position) < FastDistance)
        {
            rb.position = Vector3.MoveTowards(rb.position, player.position, Time.fixedDeltaTime * BonusSpeed);
        }
    }

}
