using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovementScript : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float airMovementSpeed = 8f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float sprintMultiplier = 1.3f;
    
    [SerializeField] float checkRadius = 0.4f; // radius of player feet
    public Transform groundCheck; // bottom of player's "feet" 
    public Transform roofCheck; //  top of player's "head"
    public LayerMask groundMask; // choose layer, making custom "ground" layer recommended, 
    // ^ used for checking ground collisions so whole environment should have the chosen layer applied

    private bool hitRoof;
    private bool isGrounded; // character controller has it's own method for checking for grounded, but it can be too wonky at times
    private Vector3 velocity;
    private float speed;


    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // This creates an invisible sphere at groundCheck.position with radius groundDistance to check for collisions with groundMask
        // Returns true if it collides
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundMask);
        hitRoof = Physics.CheckSphere(roofCheck.position, checkRadius, groundMask);

        if (isGrounded) speed = movementSpeed;
        else speed = airMovementSpeed;

        // If player's head hits roof
        if(hitRoof)
        {
            velocity.y = -2f;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;   // let's set velocity.y to a little under 0f to make sure we properly land instead of end up slightly hovering
        }

        // Setting slopeLimit to 90 degrees while jumping fixes annoying jitter bug
        // And introduces the bug of being able to climb low walls, let's call it a feature
        controller.slopeLimit = isGrounded ? 45f : 90f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Transform.right is vector facing LOCAL right, so multiplying it means we move left/right relative to player's orientation
        Vector3 move = transform.right * x + transform.forward * z;

        // Sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = movementSpeed * sprintMultiplier;
        }

        // Character Controller component takes care of moving up/down slopes/stairs etc.
        controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            // formula for velocity needed for a jump of certain height: v = sqrt(2*g*h)
            velocity.y = Mathf.Sqrt(2f*jumpHeight*-gravity);
        }

        

        // v = v-1 + a*t , where v = final velocity, v-1 = initial velocity and a = acceleration (in this case g)
        velocity.y += gravity * Time.deltaTime;

        // dy = 0.5g * t^2
        controller.Move(0.5f * velocity * Time.deltaTime);
    }
}
