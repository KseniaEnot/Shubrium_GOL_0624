using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonologueUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    private Label label;

    void Start()
    {
        //его как нибудь получать нужно чтобы не цеплять каждый раз с 0 
        //containerGameObject = GameObject.FindGameObjectWithTag("Monologue");
    }

    public void Show()
    {
        containerGameObject.SetActive(true);
        UItoolkit_unarchive();
    }

    public void Hide()
    {
        containerGameObject.SetActive(false);
    }

    public void Set_new_Sentence(string text)
    {
        label.text = text;
    }

    private void UItoolkit_unarchive()
    {
        UIDocument document = containerGameObject.GetComponent<UIDocument>();
        VisualElement visualElement = document.rootVisualElement;
        label = visualElement.Q("Label") as Label;
    }
}
