using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score=value;
            ScoreChanged.Invoke(score); 
        }
    }
    public int HighScore=0;
    public UnityEvent <int> ScoreChanged;
    public UnityEvent NewHighScoreReached;
    public void Add()
    {
        Score++;
        if(Score > HighScore)
        {
            ReachNewHighScore();
        }
    }
    public void ResetPoints()
    {
        Score = 0;
    }
    public void Set(int score)
    {
        Score=score;
        if (Score > HighScore)
        {
            ReachNewHighScore();
        }
    }

    private void ReachNewHighScore()
    {
        HighScore = Score;
        PlayerPrefs.SetInt("HighScore", Score);
        NewHighScoreReached.Invoke();
    }
}
