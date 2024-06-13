using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Scripts.Minigames.TowerGame
{
    internal class InZoneMiniGame : MiniGame
    {
        private bool IsWinning;
        private float timeToWin;
        [SerializeField]
        public List<GameZoneObjInteractable> AllInZoneObjects;
        private Coroutine WinCoroutine;
        [SerializeField]
        public GameZone GameZone;
        private void Awake()
        {
            GameZone.CountOfObjectsChanged.AddListener(OnCountChanged);
        }
        private void OnCountChanged(int newNum)
        {
            if(!playing)StartGame();
            GameProgressChanged.Invoke(newNum, AllInZoneObjects.Count);
            if (AllInZoneObjects.Count == newNum)
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
        public new void StartGame()
        {
            base.StartGame();
            playing=true;
            GameProgressChanged.Invoke(0, AllInZoneObjects.Count);
        }
        public new void StopGame()
        {
            base.StopGame();
            playing = false;
        }
    }
}
