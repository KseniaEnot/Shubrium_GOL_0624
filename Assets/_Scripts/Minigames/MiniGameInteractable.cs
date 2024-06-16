using Assets._Scripts.Minigames.EatKASHA;
using Assets._Scripts.Movement;
using Cinemachine;
using System.Collections;
using UnityEngine;
public class MiniGameInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    CinemachineVirtualCamera VirtualCamera1;
    [SerializeField]
    CinemachineVirtualCamera VirtualCamera2;
    [SerializeField]
    public MiniGame MiniGame;
    [SerializeField]
    private string Text= "CЫГРАТЬ В ИГРУ";
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
        StartCoroutine(StartPlaying());
    }

    private IEnumerator StartPlaying()
    {;   
        Player.instance.OnDialogInteract(false,false);
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Outline2>().enabled = false;
        yield return StartCoroutine(SuperPuperCameraAnimation());
        var povComponent = VirtualCamera2.GetCinemachineComponent<CinemachinePOV>();
        if (povComponent == null)
        {
            VirtualCamera2.AddCinemachineComponent<CinemachinePOV>();
        }
        MiniGame.StartGame();
        MiniGame.GameStoped.AddListener(() => {
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
    private IEnumerator SuperPuperCameraAnimation()
    {
        Player.instance.SetNewCam(VirtualCamera1);
        yield return new WaitForSeconds(2);
        Player.instance.SetNewCam(VirtualCamera2);
        yield return new WaitForSeconds(2);
    }
}