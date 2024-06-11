using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;

    public Dialogue dialogue;

    public GameObject dialogueBox;

    private Queue<string> sentences;

    public GameObject nextDialogue;

    public bool isRunning;

    public float dialogueUpdateCooldown;
    private float untilNextDialogue;

    public float dialogueStartDelay;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void Update()
    {
        if (isRunning && gameObject.activeSelf)
        {

            if (Input.GetKeyDown(KeyCode.Return) || untilNextDialogue <= 0)
            {
                dialogueBox.SetActive(true);
                DisplayNextSentence();
            }
            if (untilNextDialogue > 0)
            {
                untilNextDialogue -= Time.deltaTime;
            }
        }
        
    }

    public void StartDialogue()
    {
        isRunning = true;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        untilNextDialogue = 0 + dialogueStartDelay;
    }

    public void DisplayNextSentence()
    {
        untilNextDialogue = dialogueUpdateCooldown;
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        if (nextDialogue  != null)
        {
            nextDialogue.SetActive(true);
        }    
        gameObject.SetActive(false);
        isRunning = false;
    }
}
