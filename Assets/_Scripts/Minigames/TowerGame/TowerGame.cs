using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Minigames.TowerGame
{
    internal class TowerGame : MonoBehaviour
    {
        [SerializeField]
        private bool playing;
        private bool IsWinning;
        private float timeToWin;
        [SerializeField]
        public List<TowerCube> AllTowerCubes;
        [SerializeField]
        public List<TowerCube> CubesOnPlatform;
        private Coroutine WinCoroutine;
        private void Update()
        {
            if (playing)
            {
                if (!IsWinning)
                    if (AllTowerCubes.Count == CubesOnPlatform.Count)
                    {
                        WinCoroutine = StartCoroutine(WinCheck());
                    }
                    else
                    {
                        if (WinCoroutine != null)
                            StopCoroutine(WinCoroutine);
                        IsWinning = false;
                    }
            }
        }
        
        private IEnumerator WinCheck()
        {
            timeToWin = 3f;
            IsWinning = true;
            while(timeToWin>0)
            {
                timeToWin-= Time.deltaTime;
                yield return null;
            }
            Win();

        }
        public void Play()
        {
            playing=true;
        }
        public void Stop()
        {
            playing = false;
        }
        public void Win()
        {
            Debug.Log("TowerCreated!");
        }
    }
}
