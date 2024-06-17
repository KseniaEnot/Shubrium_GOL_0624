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
    public class Player : MonoBehaviour
    {
        public bool inDialog;
        public static Player instance;
        public PlayerMovement PlayerMovement;
        CameraController CameraController;
        PlayerInteract PlayerInteract;
        [SerializeField]
        public PauseMenuUI PlayerPauseMenuUI;
        [SerializeField]
        public CinemachineVirtualCamera originCam;
        public UnityEvent NewCamSetted;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {PlayerPauseMenuUI.Pause(); }
        }
        public void Awake()
         {
            GameController.IsPaused = false;
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
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
            rb.velocity = Vector3.zero;
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
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.freezeRotation = false;
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
