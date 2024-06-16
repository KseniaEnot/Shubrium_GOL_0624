using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Scripts.Minigames.EatKASHA
{
    internal class KashaGame : MiniGame
    {
        [SerializeField]
        public List<KashaItemInteractable> ItemsToEat;
        private int MaxItems;
        
        protected void Awake()
        {
            MaxItems = ItemsToEat.Count;
            foreach (var item in ItemsToEat)
            {
                item.GetComponent<Collider>().enabled = false;  
            }
        }
        public override void StartGame()
        {
            base.StartGame();
            foreach (KashaItemInteractable item in ItemsToEat)
            {
                item.gameObject.GetComponent<Collider>().enabled = true;
                item.Eated.AddListener(() =>
                {
                    OnItemEated(item);
                });
            }
            GameProgressChanged.Invoke(0, ItemsToEat.Count);
        }

        private void OnItemEated(KashaItemInteractable item)
        {
            ItemsToEat.Remove(item);
            GameProgressChanged.Invoke(MaxItems-ItemsToEat.Count, MaxItems);
            if (ItemsToEat.Count == 0)
            {
                StopGame();
            }
        }

        //public override void StopGame()
        //{
        //    base.StopGame();
        //}
        //public IEnumerator Stop()
        //{
        //    playing = false;
        //    yield return new WaitForSeconds(1);
        //}  
    }
}
