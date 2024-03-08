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
    private GameObject shadowUpDown;
    private GameObject shadowLeft;
    private GameObject shadowRight;

    public int speed = 5;
    void Start()
    {
        herpObj = gameObject;
        heroAnima = herpObj.GetComponent<Animator>();

        SpriteRenderer[] spriteRenderers = herpObj.GetComponentsInChildren<SpriteRenderer>();
        shadowUpDown = spriteRenderers[1].gameObject;
        shadowLeft = spriteRenderers[2].gameObject;
        shadowRight = spriteRenderers[3].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.D) && !isMovingLeft && !isMovingUp && !isMovingDown)
        {
            shadowUpDown.SetActive(false);
            shadowLeft.SetActive(false);
            shadowRight.SetActive(true);
            heroAnima.Play("Right Animation");
            movement += new Vector3(speed, 0, 0);
            isMovingRight = true;

            
        }
        else if (Input.GetKey(KeyCode.A) && !isMovingRight && !isMovingUp && !isMovingDown)
        {
            shadowUpDown.SetActive(false);
            shadowLeft.SetActive(true);
            shadowRight.SetActive(false);
            heroAnima.Play("Left Animation");
            movement -= new Vector3(speed, 0, 0);
            isMovingLeft = true;

            
        }
        else if (Input.GetKey(KeyCode.W) && !isMovingDown && !isMovingLeft && !isMovingRight)
        {
            shadowUpDown.SetActive(true);
            shadowLeft.SetActive(false);
            shadowRight.SetActive(false);
            heroAnima.Play("Up Animation");
            movement += new Vector3(0, speed, 0);
            isMovingUp = true;
        }
        else if (Input.GetKey(KeyCode.S) && !isMovingUp && !isMovingLeft && !isMovingRight)
        {
            shadowUpDown.SetActive(true);
            shadowLeft.SetActive(false);
            shadowRight.SetActive(false);
            heroAnima.Play("Down Animation");
            movement -= new Vector3(0, speed, 0);
            isMovingDown = true;
        }
        else 
        {
            shadowUpDown.SetActive(true);
            shadowLeft.SetActive(false);
            shadowRight.SetActive(false);
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


        herpObj.transform.position += movement * Time.deltaTime;
    }
}
