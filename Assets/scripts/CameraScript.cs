using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 Offset;
    public float SpeedFloat;
    public float PlayerOffsetAmount;
    public Transform Object;
    public PlayerMovement PlayerScript;

    // Use this for initialization
    void Start()
    {

    }
    private void Update()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 PlayerAdd = new Vector3(PlayerScript.BonusX, PlayerScript.BonusY, 0) * PlayerOffsetAmount;
        transform.position = Vector3.Lerp(transform.position, Object.position + Offset + PlayerAdd, SpeedFloat * Time.deltaTime);
    }

    public IEnumerator Shake(float Duration, float Magnitude)
    {

        float Elapsed = 0f;

        while (Elapsed < Duration)
        {
            float ShakeX = Random.Range(-1, 2) * Magnitude;
            float ShakeY = Random.Range(-1, 2) * Magnitude;
            transform.localPosition += new Vector3(ShakeX, ShakeY, 0);

            Elapsed += Time.deltaTime;

            yield return null;
        }

    }
}