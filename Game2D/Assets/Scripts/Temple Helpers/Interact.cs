using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{

    [SerializeField] public GameObject objectToShow;
    [SerializeField] public GameObject clue;
    [SerializeField] public List<GameObject> hideObjects;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset InkJSON;

    private DialogueSystem dialogueSystem;


    GameObject thisObj;
    GameObject hero;

    // Start is called before the first frame update
    void Awake()
    {
        thisObj = gameObject;
        hero = GameObject.FindGameObjectWithTag("Hero");
        dialogueSystem = FindObjectOfType<DialogueSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (objectToShow.active)
            return;

        if(Vector3.Distance(thisObj.transform.position, hero.transform.position) <= 2f && !objectToShow.active) 
        {
            clue.SetActive(true);
        }
        if (Vector3.Distance(thisObj.transform.position, hero.transform.position) <= 2f && Input.GetKey(KeyCode.E)) 
        {
            clue.SetActive(false);
            objectToShow.SetActive(true);
            DialogueSystem.GetInstance().EnterDialogueMode(InkJSON, SceneManager.GetActiveScene().name);

            foreach (var item in hideObjects)
            {
                item.SetActive(false);
            }
        }
    }
}
