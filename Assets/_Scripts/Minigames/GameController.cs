using Assets._Scripts.Movement;
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
    [SerializeField]
    public AudioClip QuestCompleteSOund;
    [SerializeField]
    private AudioSource audioSourceQuests;
    [SerializeField]
    private AudioSource buttonClick;
    [SerializeField]
    public AudioSource ForSounds;
    [SerializeField]
    AudioClip btnSound;
    private void Awake()
    {

        if (instance == null)
        { 
            instance = this;
            ForSounds.playOnAwake = false;
        }
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
    public void PlayQuestComplete()
    {
        audioSourceQuests.PlayOneShot(QuestCompleteSOund);
    }
    public void PlaySound(AudioClip sound)
    {
        ForSounds.PlayOneShot(sound);
    }
    public void PlayButtonSound()
    {
        buttonClick.PlayOneShot(btnSound);
    }
}
