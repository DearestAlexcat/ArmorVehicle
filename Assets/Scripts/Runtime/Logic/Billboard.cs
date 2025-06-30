using UnityEngine;

namespace ArmorVehicle
{
    public class Billboard : MonoBehaviour
    {
        Camera targetCamera;

        void Awake()
        {
            if (targetCamera == null)
                targetCamera = Camera.main;
        }

        void LateUpdate()
        {
            RotateTowardsCamera();
        }

        void RotateTowardsCamera()
        {
            if (targetCamera == null) return;

            transform.LookAt(transform.position + targetCamera.transform.forward, targetCamera.transform.up);
            //transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward, targetCamera.transform.rotation * Vector3.up);
        }
    }
}