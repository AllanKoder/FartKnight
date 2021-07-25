using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTile : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer sr; 
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
