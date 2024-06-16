using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class KsilophoneButtonInteractable : MonoBehaviour, IInteractable
{

    [SerializeField]
    public KsilophoneGame Owner;
    [SerializeField]    
    public Nota Nota;
    public  AudioClip Sound;
    private Button button;
    private AudioSource AudioSource;
    public UnityEvent<KsilophoneButtonInteractable> Clicked;
    [SerializeField]
    private Sprite OutlineSprite;
    [SerializeField]
    private Sprite NoOutlineSprite;
    private SpriteRenderer spriteRenderer;
    private Outline2 outline;
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();   
        Sound = Owner.GetNota(Nota);
        outline=GetComponent<Outline2>();
    }
    public float SoundLength { 
        get
        {
            if (Sound!=null)return Sound.length;
            return 1f;
        }
    }
    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Click()
    {
        Clicked.Invoke(this);
        Play();
    }
    public void Play()
    {
        AudioSource.PlayOneShot(Sound);
        ShowOutline();
    }
   
    private void ShowOutline() 
    {
        if(Outline.state==OutlineState.show)
        {
            if (outline != null)
            {
                outline.enabled = true;
                //spriteRenderer.sprite = OutlineSprite;
                Invoke("HideOutline", SoundLength);
            }
        }
    }
    private void HideOutline()
    {
        //spriteRenderer.sprite = NoOutlineSprite;
        outline.enabled = false;
    }

    public void Interact(Transform interactorTransform)
    {
        Click();
        Debug.Log("Ksilo pushed");
    }

    public string GetInteractText()
    {
       return Owner.GetNotaText(Nota);
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
