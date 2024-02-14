using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    private GameObject player;
    private DialogueSystem dialogueSystem;
    private Dialogue[] dialogueObject;
    public float distance;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset InkJSON;

    private void Awake()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
        dialogueObject = FindObjectsOfType<Dialogue>();
        player = GameObject.FindWithTag("Player");
    }

    //checks distance from object to person and compares it with the given max. distance (I think the name of the variables needs to be changed a little). And takes dialogue from the nearest NPC with dialogue

    private void Update()
    {
        Dialogue dialogObject = null;
        float tempDistance = distance;
        foreach (var dialog in dialogueObject) 
        {
            var distanceObject = Vector2.Distance(dialog.gameObject.transform.position, player.transform.position);
            if (distanceObject < distance) 
            {
                if (distanceObject < tempDistance) 
                {
                    tempDistance = distanceObject;
                    dialogObject = dialog;
                }
            }
        }

        //Adds LisghtDialogue indicator on NPC and staerts dialogue 
        if (dialogObject != null) 
        {
            dialogObject.gameObject.GetComponent<Animator>().SetBool(Animator.StringToHash("LightDialogue"), true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueSystem.GetInstance().EnterDialogueMode(InkJSON);
            }
        }

        //removes LisghtDialogue indicator from NPC
        foreach(var i in dialogueObject)
        {
            if (i != dialogObject)
            {
                i.gameObject.GetComponent<Animator>().SetBool(Animator.StringToHash("LightDialogue"), false);
            } 
        }
    }
}
