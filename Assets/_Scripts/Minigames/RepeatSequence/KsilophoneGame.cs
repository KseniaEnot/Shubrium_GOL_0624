using UnityEngine;
public enum Nota
{
    DO,
    RE,
    MI,
    PHA,
    SOL,
    LYA,
    SI
}
public class KsilophoneGame : SequenceControllerGame
{
    [SerializeField]
    public AudioClip DO;
    [SerializeField]
    public AudioClip RE;
    [SerializeField]
    public AudioClip MI;
    [SerializeField]
    public AudioClip PHA;
    [SerializeField]
    public AudioClip SOL;
    [SerializeField]
    public AudioClip LYA;
    [SerializeField]   
    public AudioClip SI;
    public string GetNotaText(Nota nota)
    {
        switch (nota)
        {
            case Nota.DO:
                return "ДO";
            case Nota.RE:
                return "РE";
            case Nota.MI:
                return "МИ";
            case Nota.PHA:
                return "ФА";
            case Nota.SOL:
                return "СОЛЬ";
             case Nota.LYA:
                return "ЛЯ";
            case Nota.SI :
                return "СИ";
        }
        return null;
    }
    public AudioClip GetNota(Nota nota)
    {
        switch (nota)
        {
            case Nota.DO:
                return DO;
            case Nota.RE:
                return RE;
            case Nota.MI:
                return MI;
            case Nota.PHA:
                return PHA;
            case Nota.SOL:
                return SOL;
            case Nota.LYA:
                return LYA;
            case Nota.SI:
                return SI;
        }
        return null;
    }  
}