using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Vector3 originalPos;
    Quaternion originalRot;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    //float mouseX, mouseY;
    private void Awake()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
    }

    void Update()
    {


        if (Input.GetMouseButton(1))
        {
            float rotY = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotX = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.eulerAngles += new Vector3(-rotX, rotY, 0);


            if (Input.GetKey(KeyCode.Q))
            {
                float changedValue = transform.position.y - moveSpeed * Time.unscaledDeltaTime;
                transform.position = new Vector3(transform.position.x, changedValue, transform.position.z);
            }
            if (Input.GetKey(KeyCode.E))
            {
                float changedValue = transform.position.y + moveSpeed * Time.unscaledDeltaTime;
                transform.position = new Vector3(transform.position.x, changedValue, transform.position.z);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.localPosition += transform.forward * Time.unscaledDeltaTime * moveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.localPosition -= transform.forward * Time.unscaledDeltaTime * moveSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.localPosition += transform.right * Time.unscaledDeltaTime * moveSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.localPosition -= transform.right * Time.unscaledDeltaTime * moveSpeed;
            }
  
            float valueX = transform.localPosition.x;
            float valueY = transform.localPosition.y;
            float valueZ = transform.localPosition.z;
            valueX = Mathf.Clamp(valueX, -130, -30);
            valueY = Mathf.Clamp(valueY, 40, 100);
            valueZ = Mathf.Clamp(valueZ, -120, 25);
            transform.localPosition = new Vector3(valueX, valueY, valueZ);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = originalPos;
            transform.rotation = originalRot;
        }
    }
}
