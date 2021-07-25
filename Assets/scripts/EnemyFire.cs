using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public float FireRate;
    public GameObject ShootEffect; 
    public GameObject ParticleSpawn; 
    public GameObject Projectile; 
    private float timer; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= FireRate)
        {
            Instantiate(ParticleSpawn, transform.position, Quaternion.identity);
            Instantiate(ShootEffect, transform.position, Quaternion.identity);
            Instantiate(Projectile, transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
