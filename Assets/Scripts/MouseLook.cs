using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("References")]
    public Transform cameraPivot; // Drag your PlayerCamera here in the Inspector

    [Header("Settings")]
    public float mouseSensitivity = 200f;
    public float verticalClamp = 85f;

    float xRotation = 0f;

    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical rotation (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalClamp, verticalClamp);
        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (yaw)
        transform.Rotate(Vector3.up * mouseX);
    }
}
