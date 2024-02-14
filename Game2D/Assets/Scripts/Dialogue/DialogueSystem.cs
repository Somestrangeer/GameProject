using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueSystem : MonoBehaviour
{

    [Header("DialogueUI")]
    [SerializeField] private GameObject Image;
    [SerializeField] private Text DialogueText;

    [Header("DialogueUI")]
    [SerializeField] private GameObject[] Choises;

    private Text[] ChoisesText;

    private Story CurrentSrory;

    private bool DialogueIsPlaying;

    private static DialogueSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Dialogue System Warning");
        }
        instance = this;
    }

    public static DialogueSystem GetInstance()
    {
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

    public void EnterDialogueMode(TextAsset inkJSON) 
    {
        CurrentSrory = new Story(inkJSON.text);
        DialogueIsPlaying = true;
        Image.SetActive(true);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        DialogueIsPlaying = false;
        Image.SetActive(false);
        DialogueText.text = "";
    }

    private void Update()
    {
        if (!DialogueIsPlaying)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ContinueStory();
        }
    }

    private void ContinueStory()
    {
        if (CurrentSrory.canContinue)
        {
            //set text for the current dialogue line
            DialogueText.text = CurrentSrory.Continue();

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
        List<Choice> CurrentChoises = CurrentSrory.currentChoices;

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

        for (int i = index; i < Choises.Length; i++)
        {
            Choises[i].gameObject.SetActive(false);
        }
    }

    //split text into letters
    /*IEnumerator TypeLine(string sentence)
    {
        DialogueText.text = "";
        foreach (char Letter in sentence.ToCharArray()) 
        {
            DialogueText.text += Letter;
            yield return new WaitForSeconds(0.1f);
        }
    }*/
}
