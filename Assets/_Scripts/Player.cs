using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

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
        public UnityEvent NewCamSetted;
        public void Awake()
         {
            instance = this;
            PlayerMovement=GetComponent<PlayerMovement>();
            CameraController= GetComponent<CameraController>();
            PlayerInteract = GetComponent<PlayerInteract>();
         }
        public void OnDialogInteract()
        {
            OnDialogInteract(false,false);
        }
        public void OnDialogInteract(bool camera,bool movement)
        {
            CameraController.enabled = camera;
            PlayerMovement.enabled = movement;
        }
        public void ReturnNormal(float delay)
        {
            StartCoroutine(ReturnNormalCoroutine(delay));
        }
        private IEnumerator ReturnNormalCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            ReturnNormal();
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
