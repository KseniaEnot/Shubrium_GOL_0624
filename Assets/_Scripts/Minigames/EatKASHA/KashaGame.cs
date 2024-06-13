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
        private void Awake()
        {
             MaxItems= ItemsToEat.Count;
        }
        private void Update()
        {
            if(playing)
            {
                if (ItemsToEat.Count == 0)
                {
                    StartCoroutine(Stop());
                }
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //RaycastHit hit;
                //if (Physics.Raycast(ray, out hit))
                //{
                //    Collider collider = hit.collider;
                //    if (collider != null)
                //    {
                //        KashaItem item = collider.gameObject.GetComponent<KashaItem>();
                //        if (item != null)
                //        {   
                //            item.
                //        }
                //    }
                //}
            }
        }
        public new void StartGame()
        {
            base.StartGame();
            playing=true; 
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
        }

        private new void StopGame()
        {
            base.StopGame();
            StartCoroutine(Stop()); 
        }
        public IEnumerator Stop()
        {
            playing = false;
            yield return new WaitForSeconds(1);
            GameStoped?.Invoke();
            GameStoped.RemoveAllListeners();
        }  
    }
}
