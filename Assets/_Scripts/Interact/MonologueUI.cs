using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonologueUI : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject containerGameObject;
    private Label label;

    [SerializeField] private string interactText;

    //private Transform interactor;
    private DialogueManager dialogueManager;

    void Start()
    {
        //его как нибудь получать нужно чтобы не цеплять каждый раз с 0 
        dialogueManager = gameObject.GetComponent<DialogueManager>();
        //containerGameObject = GameObject.FindGameObjectWithTag("Monologue");
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Interact " + gameObject.name);
        dialogueManager.StartDialogue();
        /*if (Input.GetKeyDown(KeyCode.Return))
        {
            dialogueManager.StartDialogue();
        }*/
        return;
    }
    public string GetInteractText()
    {
        //Debug.Log("GetInteractText");
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Show()
    {
        containerGameObject.SetActive(true);
        UItoolkit_unarchive();
    }

    public void Hide()
    {
        containerGameObject.SetActive(false);
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
}
