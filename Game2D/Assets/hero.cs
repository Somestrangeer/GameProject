using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject herpObj;
    void Start()
    {
        herpObj = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.D)) { movement += new Vector3(3, 0, 0); }
        if (Input.GetKey(KeyCode.A)) { movement -= new Vector3(3, 0, 0); }
        if (Input.GetKey(KeyCode.W)) { movement += new Vector3(0, 3, 0); }
        if (Input.GetKey(KeyCode.S)) { movement -= new Vector3(0, 3, 0); }

        // To make an animation we have to multiply it by time
        herpObj.transform.position += movement * Time.deltaTime;
    }
}
