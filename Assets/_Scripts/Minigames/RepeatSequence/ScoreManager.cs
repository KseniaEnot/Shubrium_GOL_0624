using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager
{
    public int Score=0;
    public int HighScore=0;
    public UnityEvent <int> ScoreChanged;
    public UnityEvent NewHighScoreReached;
    public void Add()
    {
        Score++;
        ScoreChanged.Invoke(Score);
        if(Score > HighScore)
        {
            ReachNewHighScore();
        }
    }
    public void ResetPoints()
    {
        Score = 0;
        ScoreChanged.Invoke(Score);
    }
    public void Set(int score)
    {
        Score=score;
        ScoreChanged.Invoke(Score);
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
