using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Movement
{
    internal class CameraController : MonoBehaviour
    {
        [Header("Camera Settings")]
        [Tooltip("Lock the cursor to the game screen on play")]
        public bool lockCursor = true;
        [Tooltip("Clamp the camera angle (Stop the camera form \"snapping its neck\")")]
        public Vector2 clampInDegrees = new Vector2(360f, 180f);
        [Tooltip("The mouse sensitivity, both x and y")]
        public Vector2 sensitivity = new Vector2(2f, 2f);
        [Tooltip("Smoothing of the mouse movement (Try with and without)")]
        public Vector2 smoothing = new Vector2(1.5f, 1.5f);
        [Tooltip("Needs to be the same name as your main cam")]
        public string cameraName = "Camera";

        private Camera cam2;
        [SerializeField]
        private CinemachineVirtualCamera cam;
        Vector2 _mouseAbsolute, _smoothMouse;
        Vector2 targetDirection, targetCharacterDirection;

        public void Awake()
        {
            cam2 = Camera.main;
            targetDirection = transform.localRotation.eulerAngles;
            targetCharacterDirection = transform.localRotation.eulerAngles;
        }
        private void Update()
        {
            cameraUpdate();
        }
        void FixedUpdate()
        {
            // Lock cursor handling
            if (lockCursor)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }
        public void cameraUpdate()
        {
            // Allow the script to clamp based on a desired target value.
            var targetOrientation = Quaternion.Euler(targetDirection);
            var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

            // Get raw mouse input for a cleaner reading on more sensitive mice.
            var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            // Scale input against the sensitivity setting and multiply that against the smoothing value.
            mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

            // Interpolate mouse movement over time to apply smoothing delta.
            _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
            _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

            // Find the absolute mouse movement value from point zero.
            _mouseAbsolute += _smoothMouse;

            // Clamp and apply the local x value first, so as not to be affected by world transforms.
            if (clampInDegrees.x < 360)
                _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

            // Then clamp and apply the global y value.
            if (clampInDegrees.y < 360)
                _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

            cam.transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;

            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
            transform.localRotation = yRotation * targetCharacterOrientation;
        }
        public void ResetPos()
        {
            cam2.transform.localPosition = new Vector3();
        }

    }
   
}
