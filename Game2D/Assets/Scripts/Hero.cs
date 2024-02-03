using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private float hp = 200f;
    private float damage = 50f;
    private int speed = 3;

    // The object of our hero
    private static GameObject hero;

    public static GameObject getHero() { return hero; }

    private void Awake()
    {
        // Get the object of hero's sprite
        hero = gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.D)) 
        {
            movement += new Vector3(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement -= new Vector3(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement += new Vector3(0, speed, 0);
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            movement -= new Vector3(0, speed, 0);
        }

        // To make an animation we have to multiply it by time
        hero.transform.position += movement * Time.deltaTime;
    }
}
