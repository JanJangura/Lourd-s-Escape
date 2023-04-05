using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Speed of our Mouse
    [Header("Mouse Sensitivity")]
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (!PauseMenu.isPaused)
        {
            // This hides and locks our cursor to the middle of the screen
            Cursor.lockState = CursorLockMode.Locked;
        }       
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            Rotation();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;    //Time.deltaTime is the amount of time since the last update was called
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // This is to look up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);  // This is to clamp the x rotation

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  // This keeps track of the x rotation.

        playerBody.Rotate(Vector3.up * mouseX); // This moves the body with the camera up and down in the x direction
    }
}
