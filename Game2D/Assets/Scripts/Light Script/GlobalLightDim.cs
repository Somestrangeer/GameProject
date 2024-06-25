using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightDim : MonoBehaviour
{
    // Start is called before the first frame update

    public static Light2D globalLight { get; set; }
    void Awake()
    {
        globalLight = GetComponent<Light2D>();
    }
}
