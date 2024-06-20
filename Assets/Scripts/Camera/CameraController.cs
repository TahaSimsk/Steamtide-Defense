using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 xBounds; // Bounds for camera movement in the X axis
    [SerializeField] private Vector2 yBounds; // Bounds for camera movement in the Y axis
    [SerializeField] private Vector2 zBounds; // Bounds for camera movement in the Z axis
    [SerializeField] private float wasdMoveSpeed; // Speed for movement with WASD keys
    [SerializeField] private float mmbMoveSpeed; // Speed for movement with the middle mouse button
    [SerializeField] private float rotationSpeed; // Speed for rotating the camera
    [SerializeField] private float zoomSpeed; // Speed for zooming in and out
    [SerializeField] private float zoomEaseSpeed; // Smoothness of the zoom transition
    [SerializeField] private GameObject partToHandleYaw; // Part of the camera to handle yaw rotation

    private Vector3 originalPosition; // Original position of the camera
    private Quaternion originalRotation; // Original rotation of the camera
    private Vector3 currentVelocity; // Velocity used for smooth zooming
    private Vector3 targetZoomPosition; // Target position for zooming

    private void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        targetZoomPosition = transform.localPosition;
    }

    private void Update()
    {
        HandleMovementWithMiddleMouseButton();
        HandleMouseWheelZoom();

        if (Input.GetMouseButton(1))
        {
            HandleRotationWithMouse();
            HandleUpAndDownMovement();
            HandleWASDMovement();
            ClampCameraPosition();
        }

        RestoreOriginalPositionAndRotation();
    }

    private void HandleMovementWithMiddleMouseButton()
    {
        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 movement = (transform.right * mouseX) + (transform.forward * mouseY);
            transform.localPosition -= movement * mmbMoveSpeed;

            ClampCameraPosition();
        }
    }

    private void RestoreOriginalPositionAndRotation()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }

    private void HandleRotationWithMouse()
    {
        float rotY = Input.GetAxis("Mouse X") * rotationSpeed;
        float rotX = Input.GetAxis("Mouse Y") * rotationSpeed;

        transform.eulerAngles += new Vector3(0, rotY, 0);
        partToHandleYaw.transform.eulerAngles += new Vector3(-rotX, 0, 0);
    }

    private void HandleWASDMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += transform.forward * Time.unscaledDeltaTime * wasdMoveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition -= transform.forward * Time.unscaledDeltaTime * wasdMoveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * Time.unscaledDeltaTime * wasdMoveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition -= transform.right * Time.unscaledDeltaTime * wasdMoveSpeed;
        }
    }

    private void HandleUpAndDownMovement()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.localPosition -= transform.up * Time.unscaledDeltaTime * wasdMoveSpeed;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.localPosition += transform.up * Time.unscaledDeltaTime * wasdMoveSpeed;
        }
    }

    private void HandleMouseWheelZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        targetZoomPosition += scrollInput * zoomSpeed * partToHandleYaw.transform.forward;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetZoomPosition, ref currentVelocity, zoomEaseSpeed, float.PositiveInfinity, Time.unscaledDeltaTime);
        targetZoomPosition = transform.localPosition;
        ClampCameraPosition();
    }

    private void ClampCameraPosition()
    {
        float clampedX = Mathf.Clamp(transform.localPosition.x, xBounds.x, xBounds.y);
        float clampedY = Mathf.Clamp(transform.localPosition.y, yBounds.x, yBounds.y);
        float clampedZ = Mathf.Clamp(transform.localPosition.z, zBounds.x, zBounds.y);

        transform.localPosition = new Vector3(clampedX, clampedY, clampedZ);
    }
}
