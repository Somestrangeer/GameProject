using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueSystem : MonoBehaviour
{

    [Header("DialogueUI")]
    [SerializeField] private GameObject Image;
    [SerializeField] private Text DialogueText;

    [Header("DialogueUI")]
    [SerializeField] private GameObject[] Choises;

    private Text[] ChoisesText;

    private Story CurrentStory;

    public bool DialogueIsPlaying;

    private static DialogueSystem instance;

    private void Awake()
    {
        instance = this;
    }


    public static DialogueSystem GetInstance()
    {
        if (instance == null)
        {
            instance = new DialogueSystem();
        }
        return instance;

    } 

    public void Start() 
    {
        Image.SetActive(false);

        DialogueIsPlaying = false;

        //get all of the choises text
        ChoisesText = new Text[Choises.Length];

        int index = 0;

        foreach (GameObject Choise in Choises) 
        {
            ChoisesText[index] = Choise.GetComponentInChildren<Text>();
            index++;
        }
    }

    //Open the dialogue panel and starts the new story with information from inkJSON file, pinned to NPC
    public void EnterDialogueMode(TextAsset inkJSON) 
    {
        CurrentStory = new Story(inkJSON.text);
        DialogueIsPlaying = true;
        Image.SetActive(true);

        ContinueStory();
    }

    //Close the dialogue panel and clear the dialogue text
    private void ExitDialogueMode()
    {
        DialogueIsPlaying = false;
        Image.SetActive(false);
        DialogueText.text = "";
    }

    private void Update()
    {
        //return right away if dialogue isn't playing
        if (!DialogueIsPlaying)
        {
            return;
        }
        // handle continuing to the next line in the dialogue when submit is pressed
        if (CurrentStory.currentChoices.Count == 0 && Input.GetKeyDown(KeyCode.R))
        {
            ContinueStory();
        }
    }

    //if the stroy can continue adds text to the dialogue panel
    //Story is a Ink.Runtime class
    public void ContinueStory()
    {
        if (CurrentStory.canContinue)
        {
            //set text for the current dialogue line
            DialogueText.text = CurrentStory.Continue();

            //display choises, if any, for this dialogue line
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> CurrentChoises = CurrentStory.currentChoices;

        //defence check to sure UI can support the number of choises
        if (CurrentChoises.Count > Choises.Length)
        {
            Debug.LogError("More choises then UI can support");
        }

        int index = 0;

        //enable and initialize the choices up to amount of choices for the line of dialogue
        foreach (Choice choice in CurrentChoises) 
        {
            Choises[index].gameObject.SetActive(true);
            ChoisesText[index].text = choice.text;
            index++;
        }

        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < Choises.Length; i++)
        {
            Choises[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoise());
    }

    private IEnumerator SelectFirstChoise()
    {
        //Event system requires we clear it first, then wait for at least one frame before we set current selected object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(Choises[0].gameObject);
    }

    public void MakeChoise(int ChoiseIndex)
    {
        CurrentStory.ChooseChoiceIndex(ChoiseIndex);

        ContinueStory();
    }
}
