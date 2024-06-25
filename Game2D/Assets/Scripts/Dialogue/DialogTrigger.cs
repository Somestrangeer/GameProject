using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    //The object of player
    private GameObject player;
    private DialogueSystem dialogueSystem;

    [Header("Visual Cue")]
    [SerializeField] private GameObject VisualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset InkJSON;

    private bool PlayerInRange;

    private void Awake()
    {
        VisualCue.SetActive(false);
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        player = GameObject.FindWithTag("Hero");
    }

    //shows the Dialogue available indicator and starts dialogue where button E pressed
    private void Update()
    {
        if (PlayerInRange && !DialogueSystem.GetInstance().DialogueIsPlaying)
        {
            VisualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                DialogueSystem.GetInstance().EnterDialogueMode(InkJSON);
            }
        }
        else
        {
            VisualCue.SetActive(false);
        }
    }

    //if player enter the collider range
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Hero")
        {
            PlayerInRange = true;
        }
    }

    //if player exit the collider range
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Hero")
        {
            PlayerInRange = false;
        }
    }

    public void StartDialgueCutScene()
    {
        DialogueSystem.GetInstance().EnterDialogueMode(InkJSON);
    }
}
