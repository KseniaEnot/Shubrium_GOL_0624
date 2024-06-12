using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {
    [SerializeField]
    public KeyCode InteractKey = KeyCode.E;
    [SerializeField]
    float interactRange = 3f;
    private void Update() {
        if (Input.GetKeyDown(InteractKey)) {
               IInteractable interactable = GetInteractableObject();
            if (interactable != null) {
                interactable.Interact(transform);
            }
            
        }
    }
    public IInteractable GetInteractableObject() {
        List<IInteractable> interactableList = new List<IInteractable>();
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray) {
            if (collider.TryGetComponent(out IInteractable interactable)) {
                interactableList.Add(interactable);
            }
        }
        IInteractable Interactable = GetClosestInteractable(interactableList) ;
        return Interactable;
    }

    private IInteractable GetClosestInteractable(List<IInteractable> interactableList)
    {
        IInteractable closestInteractable =null;
        foreach (IInteractable interactable in interactableList)
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                if (Vector3.Distance(transform.position, interactable.GetTransform().position) <
                    Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
                {
                    // Closer
                    closestInteractable = interactable;
                }
            }
        }
        return closestInteractable;
    }
}