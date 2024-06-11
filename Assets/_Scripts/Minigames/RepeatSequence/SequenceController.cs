using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SequenceController : MonoBehaviour
{
    public static SequenceController instance;
    [SerializeField]
    public int StartedSequenceLength;
    [SerializeField]
    public GameMode gameMode;
    [SerializeField]
    public int ScoreToReach;
    public Clickable[] ClickableObjects;
    public List<Clickable> ClickableSequence;
    [HideInInspector]
    private AudioSource audioSource;
    public AudioClip loseSound;
    public AudioClip winSound;
    public bool PlayFullWinSound;
    public bool IsClickable;
    public UnityEvent<RoundState> roundStateChanged;
    public ScoreManager ScoreManager = new ScoreManager();
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
    private void Start()
    {
        audioSource =GetComponent<AudioSource>();
    }
    private void Awake()
    {
        instance=this; 
        
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
                if (hit && IsClickable)
                {
                    Clickable clickable = hit.collider.GetComponent<Clickable>();
                    if (clickable != null)
                    {
                        clickable.Click();
                        OnClickableClick(clickable);
                    }

                }
            }
        }

    }
    public void SetUpGame(Clickable[] AvailableCats, int StartedSequenceLength)
    {
        this.ClickableObjects = AvailableCats;
        this.StartedSequenceLength = StartedSequenceLength;
    }
    public void StartNewGame()
    {
        ScoreManager.ResetPoints();
        CreateSequence(StartedSequenceLength);
        StartRound();
    }
    public void StartRound()
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
        Clickable clickable = ClickableObjects[num];
        ClickableSequence.Add(clickable);
        Debug.Log(clickable.Sound.ToSafeString() + " added");
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
            yield return new WaitForSeconds(ClickableSequence[i].Sound.length);
        }
        SetRoundState(roundState = RoundState.roundStarting);
        SetCatsClickable(true);
    }
    private void OnClickableClick(Clickable cat)
    {
        QueueCheck(cat);
    }
    
    private void QueueCheck(Clickable cat)
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
        ScoreManager.Set(ClickableSequence.Count);
        audioSource.PlayOneShot(winSound);
        if(PlayFullWinSound)
            yield return new WaitForSeconds(winSound.length+1f);
        else
            yield return new WaitForSeconds(1f);
        audioSource.Stop();
        yield return new WaitForSeconds(0.5f);

        if (gameMode == GameMode.byScore && ScoreManager.Score == ScoreToReach)
            Debug.Log("GameWon! Exiting...");
        AddToSequence();
        StartRound();
    }
    private void OnWrongAnswer()
    {
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
