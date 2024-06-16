using System.Collections;
using Assets._Scripts.Movement;
using Cinemachine;
using UnityEngine;

public class StartLevlInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    CinemachineVirtualCamera VirtualCamera1;
    [SerializeField]
    private float timeToWait;
    private MonologueUI dialogueManager;

    private void Start()
    {
        dialogueManager = gameObject.GetComponent<MonologueUI>();
        //StartCoroutine(CameraToPlayer());
        Interact(gameObject.transform);
    }
    public string GetInteractText()
    {
        return "";
    }
    public Transform GetTransform()
    {
        return transform;
    }
    public void Interact(Transform interactorTransform)
    {
        dialogueManager.StartDialogue();
        gameObject.GetComponent<Collider>().enabled = false;
        if (VirtualCamera1 != null)
        {
            StartCoroutine(CameraToPlayer());
        }
    }
    private IEnumerator CameraToPlayer()
    {
        Player.instance.SetNewCam(VirtualCamera1);
        Player.instance.OnDialogInteract(false, false);
        yield return new WaitForSeconds(timeToWait);
        Player.instance.OrigCameraReset();
        yield return new WaitForSeconds(timeToWait);
        Player.instance.ReturnNormal();
    }
}
