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
            
            dialogueManager.mnologueEnd.AddListener(() => {
                if (animationIsGoing) Player.instance.OnDialogInteract(false, false);
                else
                {
                    Player.instance.ReturnNormal();
                }
                }); 
            StartCoroutine(CameraToPlayer());
        }
    }
    private bool animationIsGoing=false;
    private IEnumerator CameraToPlayer()
    {
        animationIsGoing=true;
        Player.instance.OnDialogInteract(false, false);
        Player.instance.SetNewCam(VirtualCamera1);
        yield return new WaitForSeconds(1f);
        Player.instance.OrigCameraReset();
        yield return new WaitForSeconds(2f);
        Player.instance.ReturnNormal();
        animationIsGoing=false; 
    }
}
