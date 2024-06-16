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
    internal class InZoneGame : MiniGame
    {
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
            }
        }

        private IEnumerator WinCheck()
        {
            timeToWin = 3f;
            while(timeToWin>0)
            {
                timeToWin-= Time.deltaTime;
                yield return null;
            }
            Win();

        }
        public override void StartGame()
        {
            Debug.Log("StartGame");
            base.StartGame();
            playing=true;
            GameProgressChanged.Invoke(0, AllInZoneObjects.Count);
        }
        public override void StopGame()
        {
            Debug.Log("StopGame");
            base.StopGame();
            playing = false;
        }
    }
}
