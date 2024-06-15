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
        StartCoroutine(StartEating());
    }

    private IEnumerator StartEating()
    {;   
        Player.instance.OnDialogInteract(false,false);
        Player.instance.SetNewCam(VirtualCamera1);
        yield return new WaitForSeconds(1);
        Player.instance.SetNewCam(VirtualCamera2);
        yield return new WaitForSeconds(1);
        MiniGame.StartGame();
        gameObject.GetComponent<Collider>().enabled = false;
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
}