using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
public class PlayerInteractUI : MonoBehaviour {

    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerInteract playerInteract;
    //Assets._Scripts.Minigames.TowerGame.InZoneMiniGame inZoneMiniGame = ;
    //[SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;

    private void Update() {
        if ((playerInteract.HoldingObj!=null))
        {
            Show(playerInteract.HoldingObj);
        }
        else if (playerInteract.GetInteractableObject() != null) {
            Show(playerInteract.GetInteractableObject());
        } else {
            Hide();
        }
    }

    private void Show(IInteractable interactable) {
        containerGameObject.SetActive(true);
        UItoolkit_unarchive(interactable.GetInteractText());
        //interactTextMeshProUGUI.text = interactable.GetInteractText();
    }

    private void Hide() {
        containerGameObject.SetActive(false);
    }
    private void UItoolkit_unarchive(string text)
    {
        UIDocument document = containerGameObject.GetComponent<UIDocument>();
        VisualElement visualElement = document.rootVisualElement;
        Label label = visualElement.Q("Label") as Label;
        label.text = text;

        //inZoneMiniGame.GameProgressChanged.AddListener((int a, int b) => { Debug.Log("!"); });
    }



}