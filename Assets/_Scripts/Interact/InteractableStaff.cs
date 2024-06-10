using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableStaff : MonoBehaviour, IInteractable {

    [SerializeField] private string interactText;

    private void Awake() {
    }

    public void Interact(Transform interactorTransform) {
        //ChatBubble3D.Create(transform.transform, new Vector3(-.3f, 1.7f, 0f), ChatBubble3D.IconType.Happy, "Hello there!");
        Debug.Log("Hello there!");
        //animator.SetTrigger("Talk");
    }

    public string GetInteractText() {
        return interactText;
    }

    public Transform GetTransform() {
        return transform;
    }

}