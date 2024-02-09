using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Text DialogueText;
    //public Animator PressEAnimator;
    public Animator ImageAnimator;
    private Queue<string> _sentences = new Queue<string>();

    //start dialogue animation and fill queue with sentences
    public void StartDialogue(Dialogue dialogue) 
    {
        _sentences.Clear();

        DialogueText.text = "";
        ImageAnimator.SetBool(Animator.StringToHash("Start"), true);

        foreach (var sentence in dialogue.sentences) 
        {
            _sentences.Enqueue(sentence);
        }

        NextSentence();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            NextSentence();
        }
    }

    //split text into letters
    IEnumerator TypeLine(string sentence)
    {
        DialogueText.text = "";
        foreach (char Letter in sentence.ToCharArray()) 
        {
            DialogueText.text += Letter;
            yield return new WaitForSeconds(0.1f);
        }
    }



    //checking if the texts has run out
    private void NextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        string sentence = _sentences.Dequeue();
        StartCoroutine(TypeLine(sentence));
    }

    //remove dialogue panel from window when dialogue ends
    void EndDialogue()
    {
        ImageAnimator.SetBool(Animator.StringToHash("Start"), false);
    }
}
