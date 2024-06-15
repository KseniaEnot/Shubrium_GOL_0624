using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets._Scripts.Movement
{
    internal class Player : MonoBehaviour
    {
        public static Player instance;
        PlayerMovement PlayerMovement;
        CameraController CameraController;
        PlayerInteract PlayerInteract;
        [SerializeField]
        public CinemachineVirtualCamera originCam;
        public void Awake()
         {
            instance = this;
            PlayerMovement=GetComponent<PlayerMovement>();
            CameraController= GetComponent<CameraController>();
            PlayerInteract = GetComponent<PlayerInteract>();
         }
        public void OnDialogInteract()
        {
            CameraController.enabled = false;
            PlayerMovement.enabled = false;
        }
        public void ReturnNormal()
        {
            PlayerMovement.enabled = true;
            CameraController.enabled = true;
        }
        public void SetNewCam(CinemachineVirtualCamera cam)
        {
            cam.gameObject.SetActive(false);
            cam.gameObject.SetActive(true);
        }

        public void OrigCameraReset()
        {
            originCam.enabled = false;
            originCam.enabled = true;
        }
    }
}
