using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField]
    private MonologueUI monologueUI;

    private Queue<string> sentences;

    public bool isRunning = false;
    

    private void Start()
    {
        //sentences = new Queue<string>();
        monologueUI = GetComponent<MonologueUI>();
    }

    public void Update()
    {
        if (isRunning)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
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
            sentences = new Queue<string>();
            sentences.Clear();
            Debug.Log("StartDialogue " + gameObject.name);
            isRunning = true;
            foreach (string sentence in dialogue.sentences)
            {
                Debug.Log("sentence " + sentence);
                sentences.Enqueue(sentence);
            }
            Debug.Log("0? ");
            Debug.Log("monologueUI is null????? "+ (monologueUI == null));
            Debug.Log("monologueUI is null????? " + monologueUI);
            monologueUI.Show();
            Debug.Log("1? ");
            DisplayNextSentence();
            Debug.Log("2? ");

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


    public void OnTriggerEnter(Collider other)
    {
        StartDialogue();
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
