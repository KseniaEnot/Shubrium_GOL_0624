using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueAnimator : MonoBehaviour
{
    public DialogueTrigger trigger;
    
    public DialogueManager dialogueManager;

    public void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Player"))
         {
            dialogueManager.StartDialogue();
         }
    }
}
