using Assets._Scripts.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;

public class PauseMenuUI : MonoBehaviour
{
    private UIDocument document;
    private VisualElement root;
    private VisualElement PauseMenu;
    VisualElement ButtonsContainer;
        VisualElement SettingsMenu;
    private Button Resumebtn;

    private MainButtons mainButtons=new();
    private SettingsMenu settingsMenu=new();
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioMixer audioMixer;
    public void Pause()
    {
        GameController.instance.SwitchState();  
        if (GameController.IsPaused)
        {
            Show(PauseMenu);
        }
        else
        {
            Hide(PauseMenu);
            Hide(SettingsMenu);
        }
    }
    private void SwitchMenus()
    {
        Switch(ButtonsContainer);
    }
    private void Awake()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        PauseMenu = root.Q("PauseMenu");
        SettingsMenu = root.Q<TemplateContainer>("SettingsMenu");
        ButtonsContainer = PauseMenu.Q<TemplateContainer>("ButtonsContainer");
        if (PauseMenu != null) { Debug.Log("DebugLog PauseMenu not null"); }
        if (SettingsMenu != null) { Debug.Log("DebugLog SettingsMenu not null"); }
        if (ButtonsContainer != null) { Debug.Log("DebugLog ButtonsContainer not null"); }
        settingsMenu.Initialize
            (SettingsMenu, audioMixer, audioSource);
        mainButtons.Initialize(ButtonsContainer, SettingsMenu);
        Resumebtn = PauseMenu.Q("Resume") as Button;
        Resumebtn.clicked += () =>
        {
            GameController.instance.PlayButtonSound();
            Pause();
        };
        Hide(PauseMenu);
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
