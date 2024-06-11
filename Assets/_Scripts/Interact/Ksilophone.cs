using UnityEngine;

public class Ksilophone : MonoBehaviour, IInteractable
{
    AudioSource audioSource;
    [SerializeField]
    public nota Nota;
    [SerializeField]
    AudioClip DO;
    [SerializeField]
    AudioClip RE;
    [SerializeField]
    AudioClip MI;
    [SerializeField]
    AudioClip PHA;
    [SerializeField]
    AudioClip SOL;
    [SerializeField]
    AudioClip LYA;
    [SerializeField]
    AudioClip SI;
    public enum nota
    {
        DO,
        RE,
        MI,
        PHA,
        SOL,
        LYA,
        SI
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public string GetInteractText()
    {
        switch (Nota)
        {
            case nota.DO:
                return "ДO";
            case nota.RE:
                return "РE";
            case nota.MI:
                return "МИ";
            case nota.PHA:
                return "ФА";
            case nota.SOL:
                return "СОЛЬ";
             case nota.LYA:
                return "ЛЯ";
            case nota.SI :
                return "СИ";
        }
        return null;
    }

    public Transform GetTransform()
    {
        return transform;
    }
    private AudioClip GetNota()
    {
        switch (Nota)
        {
            case nota.DO:
                return DO;
            case nota.RE:
                return RE;
            case nota.MI:
                return MI;
            case nota.PHA:
                return PHA;
            case nota.SOL:
                return SOL;
            case nota.LYA:
                return LYA;
            case nota.SI:
                return SI;
        }
        return null;
    }    
    public void Interact(Transform interactorTransform)
    {
        audioSource.PlayOneShot(GetNota());
    }
}