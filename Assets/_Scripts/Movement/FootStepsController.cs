using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets._Scripts.Movement
{

   
    public class FootStepsController : MonoBehaviour
    {
        float lasttimeplayed = 0f;
        private AudioSource audioSourceForSteps;
        public float interval;
        public float playerspeedMINIMUM;
        [SerializeField]
        public List<AudioClip> steps = new List<AudioClip>();
        private void Awake()
        {
            audioSourceForSteps = GetComponent<AudioSource>();
        }
        public void Play(float playerspped)
        {
            if (playerspped < playerspeedMINIMUM) return;
            if (lasttimeplayed+ interval<Time.time)
            {
                if (steps != null && steps.Count != 0)
                {
                    audioSourceForSteps.PlayOneShot(Random(steps));
                    lasttimeplayed = Time.time;
                }
            }
        }
        public static AudioClip Random(List<AudioClip> clips)
        {
            int num = UnityEngine.Random.Range(0, clips.Count);
            return clips[num];
        }
    }
    
}
