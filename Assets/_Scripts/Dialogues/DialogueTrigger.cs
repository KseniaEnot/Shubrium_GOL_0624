using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager manager;
    public void TriggerDialogue()
    {
        manager.StartDialogue();
    }
}
