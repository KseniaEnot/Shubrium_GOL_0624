using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Scripts.Minigames.TowerGame
{
    internal class TowerZone : MonoBehaviour
    {
        [SerializeField]
        TowerGame game;
        public void OnTriggerEnter(Collider other)
        {
            var cube = other.GetComponent<TowerCube>();
            if (cube!=null)
                game.CubesOnPlatform.Add(cube);
        }
        public void OnTriggerExit(Collider other)
        {
            var cube = other.GetComponent<TowerCube>();
            if (cube != null)
                game.CubesOnPlatform.Remove(cube);
        }
    }
}
