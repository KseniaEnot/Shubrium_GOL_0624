using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class MiniGame : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent GameStoped;
    [HideInInspector]
    public UnityEvent GameStarted;
    [HideInInspector]
    public UnityEvent GameWin;
    public UnityEvent<int, int> GameProgressChanged;
    protected bool playing=false;
    public void StartGame()
    {
        GameStarted.Invoke();
        playing = true;
    }
    public void StopGame()
    {
        GameStoped.Invoke();
        playing = false;
    }
    public void Win()
    {
        GameWin.Invoke();
    }
        
}