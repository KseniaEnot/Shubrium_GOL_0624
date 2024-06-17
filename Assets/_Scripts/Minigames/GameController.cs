using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public static bool IsPaused=false;
    public static GameController instance;
    private void Awake()
    {
        if(instance == null)
        instance = this;
    }
    public void Play()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        IsPaused = false;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        Cursor.lockState = CursorLockMode.None;
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
