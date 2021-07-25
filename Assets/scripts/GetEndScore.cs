using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GetEndScore : MonoBehaviour
{
    public Text Score;
    // Start is called before the first frame update
    void Start()
    {
        Score.text = "You Reached Level: " + PlayerMovement.ProgressionLevel;
        PlayerMovement.ProgressionLevel = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
