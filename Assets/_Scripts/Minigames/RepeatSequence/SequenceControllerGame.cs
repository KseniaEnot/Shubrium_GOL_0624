using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    public AudioClip loseSound;
    public AudioClip winSound;
    public bool PlayFullWinSound;
    [HideInInspector]
    public bool IsClickable;
    public UnityEvent<RoundState> roundStateChanged;
    private ScoreManager ScoreManager = new ScoreManager();
    private RoundState roundState;
    private int CurrentNum;
    
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
    protected virtual void Start()
    {
        audioSource =GetComponent<AudioSource>();
    }
    protected virtual void Awake()
    {
    }
    public override void StartGame()
    {
        base.StartGame();
        StartNewGame();
        GameProgressChanged.Invoke(CurrentNum,ScoreToWin);
    }
    public override void StopGame()
    {
        base.StopGame();    
        SetCatsClickable(false);
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
        roundState = state;
        roundStateChanged.Invoke(roundState);
    }

    private IEnumerator PlayStartSoundSequence()
    {
        SetRoundState(RoundState.playingSounds);
        SetCatsClickable(false);
        for (int i = 0; i < ClickableSequence.Count; i++)
        {
            ClickableSequence[i].Click();
            yield return new WaitForSeconds(ClickableSequence[i].GetSoundLength());
        }
        SetRoundState(roundState = RoundState.roundStarting);
        SetCatsClickable(true);
    }
    public void OnClickableClick(KsilophoneButtonInteractable clickable)
    {
        QueueCheck(clickable);
    }
    
    private void QueueCheck(KsilophoneButtonInteractable cat)
    {
        if (CurrentNum== ClickableSequence.Count) return;
        if(CurrentNum > 0 && roundState != RoundState.playing)
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
        GameProgressChanged.Invoke(CurrentNum, ScoreToWin);
        ScoreManager.Set(ClickableSequence.Count);
        audioSource.PlayOneShot(winSound);
        if(PlayFullWinSound)
            yield return new WaitForSeconds(winSound.length+1f);
        else
            yield return new WaitForSeconds(1f);
        audioSource.Stop();
        yield return new WaitForSeconds(0.5f);

        if (gameMode == GameMode.byScore && ScoreManager.Score == ScoreToWin)
        { Win(); }
        AddToSequence();
        StartRound();
    }
    private void OnWrongAnswer()
    {
        GameProgressChanged.Invoke(0, ScoreToWin);
        CurrentNum = 0;
        audioSource.PlayOneShot(loseSound);
        SetCatsClickable(false);
        SetRoundState(roundState = RoundState.lost);
    }
    private void SetCatsClickable(bool b)
    {
        IsClickable = b;
    }
}
