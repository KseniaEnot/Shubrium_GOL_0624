using Assets._Scripts.Minigames.EatKASHA;
using Assets._Scripts.Movement;
using Cinemachine;
using System.Collections;
using UnityEngine;

public class KashaInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    CinemachineVirtualCamera VirtualCamera1;
    [SerializeField]
    CinemachineVirtualCamera VirtualCamera2;
    private KashaGame KashaGame;
    [SerializeField]
    private string Text= "ПРИСТУПИТЬ К ПОЕДАНИЮ";
    private void Awake()
    {
        KashaGame=GetComponent<KashaGame>();
    }
    public string GetInteractText()
    {
        return Text;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        StartCoroutine(StartEating());
    }

    private IEnumerator StartEating()
    {;   
        Player.instance.OnDialogInteract();
        Player.instance.SetNewCam(VirtualCamera1);
        yield return new WaitForSeconds(1);
        Player.instance.SetNewCam(VirtualCamera2);
        yield return new WaitForSeconds(1);
        KashaGame.StartGame();
        gameObject.GetComponent<Collider>().enabled = false;

        KashaGame.GameStopped.AddListener(() => {
            StartCoroutine(OnGameStop());
        });
    }
    private IEnumerator OnGameStop()
    {
        Debug.Log("GAME STOPPED");
        Player.instance.originCam.enabled = false;
        Player.instance.originCam.enabled = true;
        yield return new WaitForSeconds(2);
        Player.instance.ReturnNormal();
    }
}