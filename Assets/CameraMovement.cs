using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public float maxLookY = 15f;
    public float maxLookX = 8f;

    float camRotationY;
    float camRotationX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        camRotationY += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        camRotationY = Mathf.Clamp(camRotationY, -maxLookY, maxLookY);
        camRotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        camRotationX = Mathf.Clamp(camRotationX, -maxLookX, maxLookX);
        transform.localRotation = Quaternion.Euler(camRotationX, camRotationY, 0f);
    }
}

