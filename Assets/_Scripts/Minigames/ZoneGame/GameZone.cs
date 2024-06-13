using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Scripts.Minigames.TowerGame
{
    [RequireComponent(typeof(AudioSource))]
    internal class GameZone : MonoBehaviour
    {
        [SerializeField]
        private List<GameZoneObjInteractable> ObjectsInZone;
        public UnityEvent<int> CountOfObjectsChanged;
        private AudioClip AudioClip;
        AudioSource AudioSource;
        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
            AudioSource.playOnAwake = false;
        }
        public void OnTriggerEnter(Collider other)
        {
            GameZoneObjInteractable cube = other.GetComponent<GameZoneObjInteractable>();
            if (cube != null)
            {
                if (AudioClip != null)
                    AudioSource.PlayOneShot(AudioClip);
                ObjectsInZone.Add(cube);
                CountOfObjectsChanged.Invoke(ObjectsInZone.Count);
            }
        }
        public void OnTriggerExit(Collider other)
        {
            var cube = other.GetComponent<GameZoneObjInteractable>();
            if (cube != null)
            {
                ObjectsInZone.Remove(cube);
                CountOfObjectsChanged.Invoke(ObjectsInZone.Count);
            }
        }
    }
}
