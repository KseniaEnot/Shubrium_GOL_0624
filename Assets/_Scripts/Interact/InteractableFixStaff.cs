using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFixStaff : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText;

    private Transform interactor;
    private Rigidbody rb;
    private DialogueManager dialogueManager;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        dialogueManager = gameObject.GetComponent<DialogueManager>();
    }

    public void Interact(Transform interactorTransform)
    {
        return;
    }
    public string GetInteractText()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            dialogueManager.StartDialogue();
        }
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
