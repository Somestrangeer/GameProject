using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class hero : MonoBehaviour
{
    private GameObject herpObj;
    private Animator heroAnima;
    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isMovingUp;
    private bool isMovingDown;
    private GameObject shadowUpDown;
    private GameObject shadowLeft;
    private GameObject shadowRight;

    //Size camera in the Temple
    private GameObject objCameraSize;
    CinemachineVirtualCamera virtualCamera;
    public float targetOrthographicSize = 12f;
    public float smoothTime = 0.5f;
    private float velocity = 0f;
    private float currentOrthographicSize = 8.108678f;

    public int speed = 5;

    //private Footstep footstep;

    void Start()
    {
        herpObj = gameObject;
        heroAnima = herpObj.GetComponent<Animator>();

        SpriteRenderer[] spriteRenderers = herpObj.GetComponentsInChildren<SpriteRenderer>();
        shadowUpDown = spriteRenderers[1].gameObject;
        shadowLeft = spriteRenderers[2].gameObject;
        shadowRight = spriteRenderers[3].gameObject;

        objCameraSize = GameObject.FindGameObjectWithTag("SizeCamera");
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        bool isMoving = false; // Добавлена переменная для отслеживания движения

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

        if (Vector3.Distance(objCameraSize.transform.position, herpObj.transform.position) <= 10)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(virtualCamera.m_Lens.OrthographicSize, targetOrthographicSize, ref velocity, smoothTime);
        }
        else
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(virtualCamera.m_Lens.OrthographicSize, currentOrthographicSize, ref velocity, smoothTime);
            //virtualCamera.m_Lens.OrthographicSize = currentOrthographicSize;
        }

        //if (movement != Vector3.zero)
        //{
        //    Footstep.Instance.PlaySound(0);
        //}
        if (movement != Vector3.zero)
        {
            isMoving = true; // Установка флага движения в true, если есть движение
        }

        // Проверка флага движения перед вызовом PlaySound
        if (isMoving)
        {
            Footstep.Instance.PlaySound(0.5f); // Установка громкости на 0.5 для примера
        }
        herpObj.transform.position += movement * Time.deltaTime;

       
    }
}