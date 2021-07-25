using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health;
    private float HealthStart;
    public float DamageAnimationTimer;
    public float ColorLerpSpeed;
    public GameObject DeathAudio;
    public GameObject DeathParticles;
    public Slider HealthBar;

    private float timer;
    private SpriteRenderer sr;

    private int fartDamage;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        HealthStart = Health;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        sr.color = Color.Lerp(sr.color, Color.white, ColorLerpSpeed * Time.deltaTime);
        HealthBar.value = Health / HealthStart;
        if (Health <= 0)
        {
            PlayerMovement.AmountKilled += 1; 
            Instantiate(DeathParticles, transform.position, Quaternion.identity);
            Instantiate(DeathAudio, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Health -= FartProjectile.Damage * fartDamage * Time.deltaTime;
        if (fartDamage > 0)
        {
            DamageAnimation();
        }
    }

    void DamageAnimation()
    {
        if(timer > DamageAnimationTimer)
        {
            timer = 0;
            sr.color = Color.black;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fart"))
        {
            fartDamage++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fart"))
        {
            fartDamage--;
        }
    }
}
