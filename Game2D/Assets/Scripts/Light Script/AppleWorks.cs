using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AppleWorks : MonoBehaviour
{
    Light2D appleLight;
    float intensity = 0f;
    float target = 60f;
    float intensityChangeSpeed = 20f; // Adjust this value for desired speed
    bool alreadyDid = false;

    void Start()
    {
        appleLight = GetComponent<Light2D>();
    }

    private void Update()
    {
        if (!alreadyDid)
            IncreaseIntensity();
        else
            DecreaseIntensity();
    }

    public void IncreaseIntensity()
    {
        if (intensity < target)
        {
            intensity += intensityChangeSpeed * Time.deltaTime;
            appleLight.intensity = intensity;

        }
        else 
        {
            alreadyDid = true;
        }

    }

    public void DecreaseIntensity()
    {
        if (intensity > 0f)
        {
            intensity -= intensityChangeSpeed * Time.deltaTime;
            appleLight.intensity = intensity;
        }
    }
}
