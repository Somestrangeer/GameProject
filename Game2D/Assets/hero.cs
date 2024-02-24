using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class hero : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject herpObj;
    private Animator heroAnima;
    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isMovingUp;
    private bool isMovingDown;
    void Start()
    {
        herpObj = gameObject;
        heroAnima = herpObj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.D) && !isMovingLeft && !isMovingUp && !isMovingDown)
        {
            heroAnima.Play("Right Animation");
            movement += new Vector3(3, 0, 0);
            isMovingRight = true;
        }
        if (Input.GetKey(KeyCode.A) && !isMovingRight && !isMovingUp && !isMovingDown)
        {
            heroAnima.Play("Left Animation");
            movement -= new Vector3(3, 0, 0);
            isMovingLeft = true;
        }
        if (Input.GetKey(KeyCode.W) && !isMovingDown && !isMovingLeft && !isMovingRight)
        {
            heroAnima.Play("Up Animation");
            movement += new Vector3(0, 3, 0);
            isMovingUp = true;
        }
        if (Input.GetKey(KeyCode.S) && !isMovingUp && !isMovingLeft && !isMovingRight)
        {
            heroAnima.Play("Down Animation");
            movement -= new Vector3(0, 3, 0);
            isMovingDown = true;
        }

        // Для остановки анимации при отпускании клавиши
        if (!Input.GetKey(KeyCode.D))
        {
            isMovingRight = false;
        }
        if (!Input.GetKey(KeyCode.A))
        {
            isMovingLeft = false;
        }
        if (!Input.GetKey(KeyCode.W))
        {
            isMovingUp = false;
        }
        if (!Input.GetKey(KeyCode.S))
        {
            isMovingDown = false;
        }


        // To make an animation we have to multiply it by time
        herpObj.transform.position += movement * Time.deltaTime;
    }
}
