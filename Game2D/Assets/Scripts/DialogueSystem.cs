using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogesCollection : MonoBehaviour
{
    public string[] lines;
    public float speedText;
    public Text DialogeText;

    public int index;

    private void Start()
    {
        DialogeText.text = string.Empty;
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    //split text into letters
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray()) 
        {
            DialogeText.text += c;
            yield return new WaitForSeconds(speedText);
        }
    }

    //Skip text (go to next text)
    public void SkipText()
    {
        if(DialogeText.text == lines[index]) 
        {
            NextLines();
        }
        else
        {
            StopAllCoroutines();
            DialogeText.text = lines[index];
        }
    }

    //checking if the texts has run out
    private void NextLines()
    {
        if (index < lines.Length - 1)
        {
            index++;
            DialogeText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
