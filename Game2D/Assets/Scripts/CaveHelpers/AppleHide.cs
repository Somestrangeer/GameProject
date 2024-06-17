using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleHide : MonoBehaviour
{
    private static GameObject apple;
    // Start is called before the first frame update
    void Start()
    {
        apple = GameObject.FindGameObjectWithTag("Apple");
    }

    public void HideApple() 
    {
        apple.SetActive(false);
    }


}
