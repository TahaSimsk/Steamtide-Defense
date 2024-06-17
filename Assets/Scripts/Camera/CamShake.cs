using UnityEngine;
using System.Collections;

public class CamShake : MonoBehaviour
{
    public bool shake;
    public float shakeDuration = 0.5f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public float rotationAmount = 1.0f; // The maximum rotation angle

    Vector3 originalPos;
    Quaternion originalRot;

   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShakeCamera();
            shake = false;
        }
    }

    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }


    IEnumerator Shake()
    {
        float remainingTime = shakeDuration;

        originalPos = transform.localPosition;
        originalRot = transform.localRotation;

        while (remainingTime > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            // Random rotation using Euler angles and the rotationAmount for each axis
            transform.localRotation = originalRot * Quaternion.Euler(
                Random.Range(-rotationAmount, rotationAmount),
                Random.Range(-rotationAmount, rotationAmount),
                Random.Range(-rotationAmount, rotationAmount)
            );

            remainingTime -= Time.deltaTime * decreaseFactor;

            yield return null;
        }

        transform.localPosition = originalPos;
        transform.localRotation = originalRot;
    }
}
