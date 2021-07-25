using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public float LifeTime; 
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Death", LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
