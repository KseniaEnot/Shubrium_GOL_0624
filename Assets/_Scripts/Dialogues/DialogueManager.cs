using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue;
    private MonologueUI monologueUI;

    private Queue<string> sentences;


    public bool isRunning = false;
    

    private void Start()
    {
        sentences = new Queue<string>();
        monologueUI = gameObject.GetComponent<MonologueUI>();
    }

    public void Update()
    {
        if (isRunning)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //monologueUI.Show();
                DisplayNextSentence();
            }
        }
        
    }

    public void StartDialogue()
    {
        if (!isRunning)
        {
            Debug.Log("StartDialogue " + gameObject.name);
            isRunning = true;
            //sentences.Clear();
            //включать монолог
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            monologueUI.Show();
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
        monologueUI.Set_new_Sentence(sentence);
        Debug.Log(sentences.Count);
    }
    public void EndDialogue()
    {
        monologueUI.Hide();
        isRunning = false;
    }

}
