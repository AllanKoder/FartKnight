using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    public float speed;
    public float SetOffset;
    public float RandomOffset;
    public float turnSpeed;

    public GameObject deathParticle; 
    private Transform player; 
    private Rigidbody2D rb;

    Vector3 diff;
    float rot_z;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        diff = player.position - transform.position + new Vector3(Random.Range(-RandomOffset, RandomOffset), Random.Range(-RandomOffset, RandomOffset),0);
        diff.Normalize();

        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + SetOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (turnSpeed > 0)
        {
            Vector3 diff = player.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0f, 0f, rot_z), turnSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
