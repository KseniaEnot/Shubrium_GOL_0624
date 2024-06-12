using Assets._Scripts.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerInteract : MonoBehaviour {
    [SerializeField]
    public KeyCode InteractKey = KeyCode.E;
    [SerializeField]
    float interactRange = 3f;
    public StaffInteractable HoldingObj;
    [SerializeField]
    private bool RayMethod;
    private void Update()
    {
        if (Input.GetKeyDown(InteractKey))
        {
            {
                if (HoldingObj != null)
                {
                    HoldingObj.Interact(transform);
                    HoldingObj = null;
                    return;
                }
                IInteractable interactable = GetInteractableObject();
                if (interactable != null)
                {
                    interactable.Interact(transform);
                    if (interactable is StaffInteractable) HoldingObj = (StaffInteractable)interactable;
                }
            }

        }
    }
    public IInteractable GetInteractableObject()
    {
        {
            IInteractable Interactable;
            if (RayMethod)
            {
                Interactable = GetRayInteractable();
            }
            else
            {
                List<IInteractable> interactableList = new List<IInteractable>();
                Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
                foreach (Collider collider in colliderArray)
                {
                    if (collider.TryGetComponent(out IInteractable interactable))
                    {
                        interactableList.Add(interactable);
                    }
                }
                Interactable = GetClosestInteractable(interactableList);
            }
            return Interactable;
        }
    }
    private IInteractable GetRayInteractable()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Check if the ray hits anything
        if (Physics.Raycast(ray, out hit,interactRange))
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                return interactable;
        }
        return null;
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