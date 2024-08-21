using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceToCamera : MonoBehaviour
{
    Transform mainCam;

    private void Awake()
    {
        mainCam = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
    }
}
