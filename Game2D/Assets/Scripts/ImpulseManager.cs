using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseManager : MonoBehaviour
{
    //A row to change the speed time dynamically
    [SerializeField] private float impulseTime = 0.75f;

    private Material impulseMaterial;

    //To cahce the shader prop that we wanna change
    //Because calling it over and over is way too expansive (memory)
    //We take here the ref
    private static int impulseDistanceFromCenter = Shader.PropertyToID("_ImpulseDistanceFromCenter");

    private void Awake()
    {
        impulseMaterial = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            CallImpulse();
        }
    }

    private void CallImpulse()
    {
        //Use it to control the playing the impulse itself
        StartCoroutine(ImpulseAction(-0.1f, 1f));
    }

    private IEnumerator ImpulseAction(float startPosition, float endPosition) 
    {
        impulseMaterial.SetFloat(impulseDistanceFromCenter, startPosition);

        //Liner interpolation here begins
        //We use it to make a smooth animation per each frame
        //So don't touch this if you have no idea what it is
        float lerpedAmount = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < impulseTime) 
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(startPosition, endPosition, (elapsedTime / impulseTime));

            impulseMaterial.SetFloat(impulseDistanceFromCenter, lerpedAmount);
            yield return null;
        }
    }



}
