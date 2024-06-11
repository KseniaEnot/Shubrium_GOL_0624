using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Clickable : MonoBehaviour
{
    public int SceneID;
    public string Name;
    [SerializeField]
    public AudioClip Sound;
    [HideInInspector]
    public bool clickAvailable;
    private Button button;
    private AudioSource AudioSource;
    public UnityEvent<Clickable> Clicked;
    public bool OutlineEnabled;
    [SerializeField]
    private Sprite OutlineSprite;
    [SerializeField]
    private Sprite NoOutlineSprite;
    private SpriteRenderer spriteRenderer; 

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();   
        SetOutlineMode(true);
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetOutlineMode(bool b)
    {
        OutlineEnabled = b;
        //if (Outline.ShowOutline == true)
        //    Outline.enabled = false;

    }
    public void Click()
    {
        if (clickAvailable)
        {
            Play();
        }
    }
    private void Play()
    {
        Clicked.Invoke(this);
        AudioSource.PlayOneShot(Sound);
        ShowOutline();
    }
   
    private void ShowOutline() 
    {
        if(Outline.state==OutlineState.show)
        {
            spriteRenderer.sprite = OutlineSprite;
            Invoke("HideOutline", Sound.length);
        }
    }
    private void HideOutline()
    {
        spriteRenderer.sprite = NoOutlineSprite;
    }
}
