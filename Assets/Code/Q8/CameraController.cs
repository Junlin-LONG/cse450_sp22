using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q8 
{
    public class CameraController : MonoBehaviour
    {
        // Outlet
        public Transform target;

        // Configuration
        public Vector3 offset;
        public float smoothness;

        // State Tracking
        Vector3 _velocity;

        // Methods
        private void Start()
        {
            if (target)
            {
                offset = transform.position - target.position;
            }
        }

        private void Update()
        {
            if (target)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref _velocity, smoothness);
            }
        }
    }
}

