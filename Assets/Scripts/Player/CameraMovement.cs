using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float minClamp = -90.0f;
    public float maxClamp = 90.0f;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseSensitivity = GameSettings.MouseSensitivity;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minClamp, maxClamp);

        yRotation += mouseX;

        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0f);
    }
}