using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableStaff : MonoBehaviour, IInteractable {

    [SerializeField] private string interactText;
    private StaffMode StaffMode= StaffMode.none;
    private Transform interactor;
    private Rigidbody rb;
    private float range;
    private bool Charging;
    private float chargeTime;
    private void Awake() {
        rb= gameObject.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if(StaffMode == StaffMode.grabing)
        {
            if (interactor != null)
            {
                float lerpSpeed = 10f;
                Vector3 newPos = Vector3.Lerp(transform.position, Camera.main.transform.position+ Camera.main.transform.forward* range, Time.deltaTime * lerpSpeed);
                if (rb)
                    rb.MovePosition(newPos);
                else
                    transform.position = newPos;
            }
            if (Input.GetKey(KeyCode.C))
            {
                Charging = true;
                chargeTime += Time.deltaTime;
            }
            else if(Charging)
            {
                Throw();
            }


        }
    }

    private void Throw()
    {
        Drop();
        rb.AddForce((Camera.main.transform.forward+Vector3.up/2) * chargeTime*300f);
        chargeTime = 0;
        Charging=false;
    }

    public void Interact(Transform interactorTransform)
    {
        if (StaffMode == StaffMode.none)
            Pickup(interactorTransform);
        else
            Drop();
        //animator.SetTrigger("Talk");
    }
    private void Pickup(Transform interactorTransform)
    {
        if (rb)
            rb.useGravity = false;
        interactor = interactorTransform;
        range = (interactor.position - transform.position).magnitude;
        StaffMode = StaffMode.grabing;
    }
    private void Drop()
    {
        rb.useGravity = true;
        interactor = null;
        StaffMode = StaffMode.none;
    }

    public string GetInteractText() {
        return interactText;
    }

    public Transform GetTransform() {
        return transform;
    }

}
enum StaffMode
{
    grabing,
    none
}