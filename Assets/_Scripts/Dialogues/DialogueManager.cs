using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public TMP_Text dialogueText;

    public Dialogue dialogue;
    //канвас
    //public GameObject dialogueBox;
    private MonologueUI monologueUI;

    private Queue<string> sentences;

    public GameObject nextDialogue;

    public bool isRunning = false;
    

    private void Start()
    {
        sentences = new Queue<string>();
        monologueUI = gameObject.GetComponent<MonologueUI>();
    }

    public void Update()
    {
        if (isRunning && gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                monologueUI.Show();
                //dialogueBox.SetActive(true);
                DisplayNextSentence();
            }
        }
        
    }

    public void StartDialogue()
    {
        if (!isRunning)
        {
            isRunning = true;

            sentences.Clear();
            //включать монолог
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        // вывод текста сюда
        monologueUI.Set_new_Sentence(sentence);
        //StopAllCoroutines();
        //StartCoroutine(TypeSentence(sentence));
    }
    /* пережитки канваса
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }*/
    public void EndDialogue()
    {
        //dialogueBox.SetActive(false);
        monologueUI.Hide();
        if (nextDialogue  != null)
        {
            nextDialogue.SetActive(true);
        }    
        //gameObject.SetActive(false);
        isRunning = false;
    }

}
