using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public bool IsPaused;
    public static GameController instance;
    private void Awake()
    {
        instance = this;
    }
    public void Play()
    {
        Time.timeScale = 0;
        IsPaused = false;
    }
    public void Pause()
    {
        Time.timeScale = 1f;
        IsPaused = true;
    }
    public void SwitchState()
    {

        if (IsPaused)
        {
            Play();
        }
        else Pause();
    }
}
