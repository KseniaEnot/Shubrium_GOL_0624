using Assets._Scripts.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Scripts.Minigames.EatKASHA
{
    public class KashaItemInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        public string Text;
        public UnityEvent OnInteract;
        public UnityEvent Eated;
        public bool disableOnInteract = true;

        public List<AudioClip> clips;
        private void Awake()
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
        public string GetInteractText()
        {
            return Text;
        }

        public Transform GetTransform()
        {
           return transform;
        }

        public void Interact(Transform interactorTransform)
        {
            OnInteract.Invoke();
            if(disableOnInteract) gameObject.SetActive(false);
            Eated?.Invoke();
            if (clips != null && clips.Count != 0)
            GameController.instance.PlaySound(FootStepsController.Random(clips));
        }
    }
}
