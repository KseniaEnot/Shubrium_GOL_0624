using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class PauseMenuUI : MonoBehaviour
{
    private UIDocument document;
    private VisualElement root;
    private VisualElement PauseMenu;
    VisualElement a;
        VisualElement b;
    private Button Resumebtn;

    private MainButtons mainButtons = new();
    private SettingsMenu settingsMenu = new();
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioMixer audioMixer;
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            GameController.instance.SwitchState();
            if(GameController.instance.IsPaused)
            {
                Hide(a);
                Hide(b);
            }
            else
            {

                Show(a);
            }
        }
    }

    private void SwitchMenus()
    {
        Switch(a);
    }
   
    private void Start()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        b = root.Q<TemplateContainer>("SettingsMenu");
        settingsMenu.Initialize
            (b, audioMixer, audioSource);

        PauseMenu = root.Q("PauseMenu");
        Resumebtn = PauseMenu.Q("Resume") as Button;
        a = PauseMenu.Q<TemplateContainer>("ButtonsContainer");
        mainButtons.Initialize(a, b);

        Resumebtn.clicked += () =>
        {
            GameController.instance.Play();
            Hide(a);
            Hide(b);
        };
    }
    private void Hide(VisualElement a)
    {
        a.style.display = DisplayStyle.None;
    }
    private void Switch(VisualElement a)
    {
        if (a.style.display == DisplayStyle.Flex)
            a.style.display = DisplayStyle.None;
        else
            a.style.display = DisplayStyle.Flex;
    }
    private void Show(
        VisualElement a)
    {
         a.style.display = DisplayStyle.Flex;
    }
}
