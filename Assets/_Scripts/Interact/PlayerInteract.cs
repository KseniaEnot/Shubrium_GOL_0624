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
    public KeyCode ThrowKey = KeyCode.V;
    [SerializeField]
    float interactRange = 3f;
    public StaffInteractable HoldingObj;
    [SerializeField]
    private bool RayMethod;
    private Outline2 _lastObj;
    private bool Charging;
    private float chargeTime;
    private void Update()
    {
        setOutline();
        if (Input.GetKeyDown(InteractKey))
        {
            if (Input.GetKey(ThrowKey))
            {
                if (HoldingObj != null)
                {
                    Charging = true;
                    chargeTime += Time.deltaTime;
                }
            }
            else if (Charging && HoldingObj != null)
            {
                HoldingObj.Throw(chargeTime);
                chargeTime = 0;
                Charging = false;
                Drop();

            }
            if (Input.GetKeyDown(InteractKey))
            {
                if (HoldingObj != null)
                {
                    HoldingObj.Interact(transform);
                    Drop();
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
    private void Drop()
    {
        HoldingObj = null;
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
    
    private void setOutline()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Check if the ray hits anything
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.gameObject.GetComponent<Outline2>() != null)
            {
                if (_lastObj != null)
                {
                    _lastObj.enabled = false;
                }
                _lastObj = hit.collider.gameObject.GetComponent<Outline2>();
                _lastObj.enabled = true;

            }
            else if (_lastObj != null)
            {
                _lastObj.enabled = false;
                _lastObj = null;
            }
        }
        else if (_lastObj != null)
        {
            _lastObj.enabled = false;
            _lastObj = null;
        }
    }

    private IInteractable GetRayInteractable()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Check if the ray hits anything
        if (Physics.Raycast(ray, out hit, interactRange)) { 

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