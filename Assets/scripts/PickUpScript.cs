using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public Image colorChanger; 
    public Text ItemName; 
    public Text ItemDescriptions; 
    public Text TimerText; 
    
    private PlayerMovement pm;
    private float timer;
    private int timerOn;

    float fd;
    float bm;
    float fr;
    float rr;
    float ft;
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        timerOn = 0;
        ItemName.color = Color.clear;
        ItemDescriptions.color = Color.clear;
        TimerText.text = "";

        fd = pm.FartDamage;
        bm = pm.BonusMovement;
        fr = pm.FartReload;
        rr = pm.RegenRate;
        ft = pm.FartTotal;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn == 1)
        {
            timer -= Time.deltaTime;
            TimerText.text = timer.ToString("0.0");
            if (timer <= 0)
            {
                pm.RandomColorRange = new float[] {0.2f, 0.3f, 1f, 1f, 0.5f, 1f, 0.8f, 1f};
                pm.FartDamage = fd;
                pm.BonusMovement = bm;
                pm.FartReload = fr;
                pm.RegenRate = rr;
                pm.RegenRate = rr;
                pm.FartTotal = ft;

                colorChanger.color = new Color32(0, 204, 19, 255);
                timerOn = 0;
            }
        }
        if(timerOn == 0)
        {
            ItemName.color = Color.Lerp(ItemName.color, Color.clear, 1f * Time.deltaTime);
            ItemDescriptions.color = Color.Lerp(ItemDescriptions.color, Color.clear, 1f * Time.deltaTime);
            TimerText.text = "";
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            GameObject pickup = collision.gameObject;
            if (pickup.name.Contains("Pizza"))
            {
                pm.RandomColorRange =  new float[] {0.1f, 0.2f, 1f, 1f, 0.5f, 1f, 0.8f, 1f};
                pm.FartDamage =  1.5f;
                colorChanger.color = new Color32(148, 124, 35, 255);
                Destroy(pickup);
                timer = 10;
                timerOn = 1;

                ItemName.color = Color.white;
                ItemDescriptions.color = Color.white;
                ItemName.text = "Pizza";
                ItemDescriptions.text = "1.5x stronger farts";
            }
            if (pickup.name.Contains("Eggplant"))
            {
                pm.RandomColorRange =  new float[] {0.7f, 0.8f, 1f, 1f, 0.5f, 1f, 0.8f, 1f};
                pm.BonusMovement =  100;
                colorChanger.color = new Color32(0, 81, 255,255);

                Destroy(pickup);
                timer = 10;
                timerOn = 1;

                ItemName.color = Color.white;
                ItemDescriptions.color = Color.white;
                ItemName.text = "Eggplant";
                ItemDescriptions.text = "1.5x faster speed with farts";

            }
            if (pickup.name.Contains("Potato"))
            {
                pm.FartReload = 0.1f;
                pm.refreshTimer = 0;
                Destroy(pickup);

                timer = 10;
                timerOn = 1;

                ItemName.color = Color.white;
                ItemDescriptions.color = Color.white;
                ItemName.text = "Potato";
                ItemDescriptions.text = "Almost instant reload";
            }
            if (pickup.name.Contains("Taco"))
            {
                pm.FartTotal = 1.5f;
                pm.RandomColorRange =  new float[] {0.6f, 0.7f, 1f, 1f, 0.5f, 1f, 0.8f, 1f};
                colorChanger.color = new Color32(0, 81, 255,255);
                Destroy(pickup);
                timer = 10;
                timerOn = 1;
                ItemName.color = Color.white;
                ItemDescriptions.color = Color.white;
                ItemName.text = "Taco";
                ItemDescriptions.text = "1.5x fart total";
            }
            if (pickup.name.Contains("Apple"))
            {
                pm.RegenRate = 5f;
                pm.RandomColorRange =  new float[] {0f, 0.1f, 1f, 1f, 0.5f, 1f, 0.8f, 1f};
                Destroy(pickup);
                timer = 10;
                timerOn = 1;
                ItemName.color = Color.white;
                ItemDescriptions.color = Color.white;
                ItemName.text = "Apple";
                ItemDescriptions.text = "5x regen speed";
            }
        }
    }
}
