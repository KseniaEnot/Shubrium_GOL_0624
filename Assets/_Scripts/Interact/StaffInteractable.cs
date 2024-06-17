using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StaffInteractable : MonoBehaviour, IInteractable {

    [SerializeField]
    private string interactText;
    [SerializeField]
    private float RotationSpeed=3f;
    public StaffMode StaffModeNow= StaffMode.none;
    private Transform interactor;
    private Rigidbody rb;
    private float range;
    private void Awake() {
        rb= gameObject.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (GameController.IsPaused) return;
        if (StaffModeNow == StaffMode.grabing)
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
            
            if (Input.GetKey(KeyCode.X))
                transform.Rotate(new Vector3(RotationSpeed, 0f), Space.World);
            if (Input.GetKey(KeyCode.Z))
                transform.Rotate(new Vector3(0f, 0f, RotationSpeed) , Space.World);
            if (Input.GetKey(KeyCode.C))
                transform.Rotate(new Vector3(0f, RotationSpeed, 0f) , Space.World);
        }
    }

    private void Rotate()
    {
        if(Input.GetKey(KeyCode.X))
            transform.Rotate(new Vector3(1f, 0f) * Time.deltaTime, Space.World);
        if(Input.GetKey(KeyCode.Z))
            transform.Rotate(new Vector3(0f, 0f,1f) * Time.deltaTime, Space.World);
        if( Input.GetKey(KeyCode.C))
            transform.Rotate(new Vector3(0f, 1f, 0f) * Time.deltaTime, Space.World);

    }
    public void Interact(Transform interactorTransform)
    {
        if (StaffModeNow == StaffMode.none)
            Pickup(interactorTransform);
        else
            Drop();
        //animator.SetTrigger("Talk");
    }
    public void Throw(float force)
    {
        Drop();
        rb.AddForce((Camera.main.transform.forward + Vector3.up / 2) * force * 300f);
    }
    private void Pickup(Transform interactorTransform)
    {
        if (rb)
            rb.useGravity = false;
        //поворот тоже отклбчать надо, а то его колбасит жутко кто сказал?? так и надо!!!!! в следующий раз я точно не замечу комментарий
        interactor = interactorTransform;
        range = (interactor.position - transform.position).magnitude;
        StaffModeNow = StaffMode.grabing;
    }
    public void Drop()
    {
        rb.useGravity = true;
        interactor = null;
        StaffModeNow = StaffMode.none;
    }

    public string GetInteractText() {
        if (StaffModeNow == StaffMode.grabing)
            return "Бросить";
        return interactText;
    }

    public Transform GetTransform() {
        return transform;
    }

}
public enum StaffMode
{
    grabing,
    none
}