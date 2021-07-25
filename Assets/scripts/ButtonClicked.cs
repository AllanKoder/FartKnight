using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClicked : MonoBehaviour
{
    public GameObject TitleImage;
    public GameObject PlayButton;
    public GameObject BlackBG;
    public GameObject StoryText;
    public GameObject SkipButton;
    public float Scrolltime;
    private bool Scrolling;
    private float ScrollAmount;

    // Start is called before the first frame update
    void Start()
    {
        Scrolling = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Scrolling)
        {
            Scrolltime -= Time.deltaTime;
            Vector3 p = StoryText.transform.position;
            p.y = StoryText.transform.position.y + ScrollAmount * Time.deltaTime;
            StoryText.transform.position = p;
            if(Scrolltime <= 0)
            {
                SceneManager.LoadScene ("Level");
            }
        }
    }

    public void OnGui()
    {
    }

    public void OnButtonPress()
    {
        print("pressed");
        TitleImage.SetActive(false);
        PlayButton.SetActive(false);
        BlackBG.SetActive(true);
        StoryText.SetActive(true);
        SkipButton.SetActive(true);

        Scrolling = true;
        RectTransform rt = StoryText.GetComponent (typeof (RectTransform)) as RectTransform;
        ScrollAmount = rt.rect.height/Scrolltime;
    }
}
