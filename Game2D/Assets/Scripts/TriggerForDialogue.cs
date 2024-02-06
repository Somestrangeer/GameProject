using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger1 : MonoBehaviour
{
    public GameObject Canvas;

    //calling the dialog window prefab when the trigger fires
    private void OnTriggerEnter2D(Collider2D collision)
    {
       Instantiate(Canvas, new Vector3(1, 1, 1), Quaternion.identity);
    }
}
