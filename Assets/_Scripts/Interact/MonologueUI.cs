using System.Collections;
using System.Collections.Generic;
//using Assets._Scripts.Movement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MonologueUI : MonoBehaviour, IInteractable
{
    
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private string interactText;
    [SerializeField] private MiniGame game;
    private Label label;
    [SerializeField] private Dialogue dialogue;
    public UnityEvent mnologueEnd;

    private Queue<string> sentences;

    public bool isRunning = false;

    void Start()
    {
        //его как нибудь получать нужно чтобы не цеплять каждый раз с 0 
        //containerGameObject = GameObject.FindGameObjectWithTag("Monologue");
        //Debug.Log(containerGameObject==null);
    }
    public void Update()
    {
        if (isRunning)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                DisplayNextSentence();
            }
        }

    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Interact " + gameObject.name);
        StartDialogue();
        return;
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
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
                sentences.Enqueue(sentence);
                Debug.Log(sentence);
            }
            Debug.Log("1");
            Show();
            Debug.Log("2");
            DisplayNextSentence();

        }
    }
    public void EndDialogue()
    {
        Hide();
        if (game!=null)
            game.StartGame();
        isRunning = false;
        mnologueEnd.Invoke();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        Set_new_Sentence(sentence);
    }
    
    public void Show()
    {
        Debug.Log("3");
        containerGameObject.SetActive(true);
        //Player.instance.OnDialogInteract();
        Debug.Log("4");
        UItoolkit_unarchive();
    }

    public void Hide()
    {
        containerGameObject.SetActive(false);
        //Player.instance.ReturnNormal();
    }

    public void Set_new_Sentence(string text)
    {
        label.text = text;
    }

    private void UItoolkit_unarchive()
    {
        UIDocument document = containerGameObject.GetComponent<UIDocument>();
        VisualElement visualElement = document.rootVisualElement;
        label = visualElement.Q("Label") as Label;
    }
    public void OnTriggerEnter(Collider other)
    {
        StartDialogue();
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
