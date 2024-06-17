using Assets._Scripts.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

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
    private Outline2 _lastObjOutline;
    private bool Charging;
    private float chargeTime;
    private void Update()
    {
        if (GameController.IsPaused) return;
        setOutline();
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
    private void Drop()
    {
        HoldingObj = null;
    }

    public IInteractable GetInteractableObject()
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

    private void setOutline()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            Outline2 outline = hit.collider.gameObject.GetComponent<Outline2>();
            if (outline != null)
            {
                if (_lastObjOutline == outline)
                    return;
                HideLastOutline();
                ShowOutline(outline);
            }
        }
        else HideLastOutline();

    }
    public enum OutlineShowMode
    {
        enable,
        setWidth,
        mode
    }
    public OutlineShowMode outlineMode;
    void ShowOutline(Outline2 outline)
    {
        if (outline.enabled == false)
        {
            outline.enabled=true;
        }
        switch (outlineMode)
        {
            case OutlineShowMode.enable:
                outline.enabled = true;
                break;
            case OutlineShowMode.setWidth:
                outline.OutlineWidth = 2f;
                break;
            case OutlineShowMode.mode:
                outline.OutlineMode = Outline2.Mode.OutlineVisible;
            break;
        }
        _lastObjOutline = outline;
    }
    private void HideLastOutline()
    {
        if (_lastObjOutline == null)
            return;
        switch (outlineMode)
        {
            case OutlineShowMode.enable:
                _lastObjOutline.enabled = false;
                break;
            case OutlineShowMode.setWidth:
                _lastObjOutline.OutlineWidth = 0f;
                break;
            case OutlineShowMode.mode:
                _lastObjOutline.OutlineMode = Outline2.Mode.OutlineHidden;
                break;
        }
        _lastObjOutline= null;
    }
    private IInteractable GetRayInteractable()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
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