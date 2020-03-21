using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookControls : MonoBehaviour
{
    public Transform playerCamera;
    private float mouseSensitivity = 200f;
    private float pitchRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        pitchRotation -= mouseY;
        pitchRotation = Mathf.Clamp(pitchRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(pitchRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void setSensitivity(float value)
    {
        mouseSensitivity = value;
    }
}
