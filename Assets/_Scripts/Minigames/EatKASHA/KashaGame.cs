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
    internal class KashaGame : MonoBehaviour
    {
        public UnityEvent GameStopped;
        [SerializeField]
        public List<KashaItem> ItemsToEat;
        private bool playing;
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
        public void StartGame()
        {
            playing=true; 
            foreach (KashaItem item in ItemsToEat)
            {
                item.gameObject.GetComponent<Collider>().enabled = true;
                item.Eated.AddListener(() => { ItemsToEat.Remove(item); });
            }
        }
        public IEnumerator Stop()
        {
            playing = false;
            yield return new WaitForSeconds(1);
            GameStopped?.Invoke();
            GameStopped.RemoveAllListeners();
        }  
    }
}
