using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private UIDocument document;
    private VisualElement root  ;
    private MainButtons mainButtons = new();
    private SettingsMenu settingsMenu=new();
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioMixer audioMixer;
    private void Start()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        VisualElement b = root.Q<TemplateContainer>("SettingsMenu");
        settingsMenu.Initialize
            (b,audioMixer,audioSource);
        VisualElement a = root.Q<TemplateContainer>("ButtonsContainer");
        mainButtons.Initialize(a, b);
    }
}
