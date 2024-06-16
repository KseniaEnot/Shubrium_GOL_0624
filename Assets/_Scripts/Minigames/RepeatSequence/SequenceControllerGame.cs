using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public enum RoundState
{
    gameStarting,
    playingSounds,
    roundStarting,
    playing,
    lost,
}
public enum GameMode
{
    endless,
    byScore,
    byTimeINDEVELOPINGDONTUSE
}
public class SequenceControllerGame : MiniGame
{
    [SerializeField]
    public int StartedSequenceLength;
    [SerializeField]
    public GameMode gameMode;
    [SerializeField]
    public int ScoreToWin;
    public KsilophoneButtonInteractable[] ClickableObjects;
    public List<KsilophoneButtonInteractable> ClickableSequence;
    [HideInInspector]
    protected AudioSource audioSource;
    [SerializeField]
    public AudioClip loseSound;
    [SerializeField]
    public AudioClip RoundWinSound;
    [SerializeField]
    public AudioClip GameWinSound;
    public bool PlayFullWinSound;
    [HideInInspector]
    public bool IsClickable;
    public UnityEvent<RoundState> roundStateChanged;
    protected ScoreManager ScoreManager;
    private bool stop;
    protected RoundState RoundState
    {
        get { return roundState; }
        set
        {
            roundState = value;
            roundStateChanged.Invoke(roundState);
        }
    }
    protected RoundState roundState;
    protected int CurrentNum;
   
    protected virtual void Start()
    {
        audioSource =GetComponent<AudioSource>();
    }
    protected void Awake()
    {
        foreach (var item in ClickableObjects)
        {
            item.Clicked.AddListener(OnClickableClick);
        }
        SetCatsClickable(false);
        ScoreManager = GetComponent<ScoreManager>();
        Outline.state = OutlineState.show;
    }
    public override void StartGame()
    {
        base.StartGame();
        StartNewGame();
        GameProgressChanged.Invoke(CurrentNum,ScoreToWin);
    }
    public override void StopGame()
    {
        SetCatsClickable(false);
        audioSource.Stop();
        stop=true;
        base.StopGame();    
    }
    public void SetUpGame(KsilophoneButtonInteractable[] AvailableCats, int StartedSequenceLength)
    {
        this.ClickableObjects = AvailableCats;
        this.StartedSequenceLength = StartedSequenceLength;
    }
    private void StartNewGame()
    {
        ScoreManager.ResetPoints();
        CreateSequence(StartedSequenceLength);
        StartRound();
    }
    private void StartRound()
    {
        SetRoundState(RoundState.roundStarting);
        CurrentNum = 0;
        StartCoroutine(PlayStartSoundSequence());
    }
    private void CreateSequence(int count)
    {
        CurrentNum = 0;
        ClickableSequence.Clear();
        for (int i = 0; i < count; i++)
        {
            AddToSequence();
        }
    }
    private void AddToSequence()
    {
        int num = UnityEngine.Random.Range(0, ClickableObjects.Length);
        KsilophoneButtonInteractable clickable = ClickableObjects[num];
        ClickableSequence.Add(clickable);
    }
    private void SetRoundState(RoundState state)
    {
        RoundState = state;
    }

    private IEnumerator PlayStartSoundSequence()
    {
        SetRoundState(RoundState.playingSounds);
        SetCatsClickable(false);
        for (int i = 0; i < ClickableSequence.Count; i++)
        {
            ClickableSequence[i].Play();
            yield return new WaitForSeconds(ClickableSequence[i].SoundLength);
            if (stop)
                break;
        }
        SetRoundState(RoundState = RoundState.roundStarting);
        SetCatsClickable(true);
    }
    public void OnClickableClick(KsilophoneButtonInteractable clickable)
    {
        QueueCheck(clickable);
    }
    
    private void QueueCheck(KsilophoneButtonInteractable cat)
    {
        if (CurrentNum== ClickableSequence.Count) return;
        if(CurrentNum > 0 && RoundState != RoundState.playing)
            SetRoundState(RoundState.playing);
        if(cat == ClickableSequence[CurrentNum])
            OnRightAnswer();
        else
            OnWrongAnswer();
    }
    private void OnRightAnswer()
    {
        if(CurrentNum == ClickableSequence.Count-1)
        {
            StartCoroutine(OnRoundWin());
        }
        CurrentNum++;
    }
    private IEnumerator OnRoundWin()
    {
        ScoreManager.Set(ClickableSequence.Count);
        audioSource.PlayOneShot(RoundWinSound);
        if(PlayFullWinSound)
            yield return new WaitForSeconds(RoundWinSound.length+1f);
        else
            yield return new WaitForSeconds(1f);
        audioSource.Stop();
        yield return new WaitForSeconds(0.5f);
        GameProgressChanged.Invoke(CurrentNum, ScoreToWin);
        if (ScoreManager.Score == ScoreToWin)
        {
            Win();
        }
        else
        {
            AddToSequence();
            StartRound();
        }
    }
    public override void Win()
    {
        StopGame();
        base.Win();
        audioSource.PlayOneShot(GameWinSound);
        gameMode = GameMode.endless;
        ScoreToWin = 1000;
    }
    private void OnWrongAnswer()
    {
        GameProgressChanged.Invoke(0, ScoreToWin);
        CurrentNum = 0;
        audioSource.PlayOneShot(loseSound);
        SetCatsClickable(false);
        SetRoundState(RoundState = RoundState.lost);
        StopGame();
    }
    private void SetCatsClickable(bool b)
    {
        foreach(var cat in ClickableObjects)
        {
            cat.GetComponent<Collider>().enabled = b;
        }
    }
}
