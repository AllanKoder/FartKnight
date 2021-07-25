using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonStopPlayer : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
