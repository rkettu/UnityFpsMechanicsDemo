using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCameraScript : MonoBehaviour
{
    [SerializeField] float mouseSens = 1000f;

    public Transform playerBody;
    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Disables (locks) cursor
        // Hint: esc to re-enable cursor in inspector 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting and storing mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        // Remember: confusingly enough, the x axis is actually the pitch rotation and y axis the yaw rotation

        xRotation -= mouseY;                            // += for inverted camera pitch controls
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // so rotation stays between -90 and +90 degrees
        //gunRotation = Mathf.Clamp(-mouseY, 0.3f, 0.7f);
        
        // Unsurprisingly, sets camera pitch rotation according to xRotation 
        this.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Vector3.up returns Vector3(0,1,0), so multiplying it only changes one value, therefore player's body stays upright
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
