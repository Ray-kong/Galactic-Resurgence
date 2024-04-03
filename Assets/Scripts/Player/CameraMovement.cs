using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float mouseSensitivity = 100.0f; // Sensitivity of mouse movement
    public float minClamp = -90.0f; // Minimum vertical rotation
    public float maxClamp = 90.0f; // Maximum vertical rotation

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // Invert Y axis
        xRotation = Mathf.Clamp(xRotation, minClamp, maxClamp); // Clamp vertical rotation

        yRotation += mouseX;

        // Apply rotation to the camera
        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0f);

        //transform.position = playerTransform.position;
    }
}