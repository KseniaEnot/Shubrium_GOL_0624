using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainButtons
{
    //private UIDocument document;
    private VisualElement container;
    private Button Playbtn;
    private Button Loadbtn;
    private Button Settingsbtn;
    private Button Exitbtn;
    private VisualElement SettingsMenu;
    public void Initialize(VisualElement root, VisualElement SettingsMenu)
    {
        container=root;
        //this.document = document;
        Playbtn = container.Q("Play") as Button;
        Loadbtn = container.Q("Load") as Button;
        Settingsbtn = container.Q("Settings") as Button;
        Exitbtn = container.Q("Exit") as Button;
        Playbtn.clicked += () =>
        {
            Loader.Load(Loader.Scene.s1_home);
        };
        Loadbtn.clicked += () =>
        {
            Debug.Log("Not implemented");
        };
        SettingsMenu.style.display = DisplayStyle.None;
        Settingsbtn.clicked += () =>
        {
            if(SettingsMenu.style.display == DisplayStyle.Flex)
                SettingsMenu.style.display= DisplayStyle.None;
            else
                SettingsMenu.style.display= DisplayStyle.Flex;
        };
        Exitbtn.clicked += () =>
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
                Application.Quit();
            else
                Loader.Load(Loader.Scene.MainMenu);
        };
    }
}
