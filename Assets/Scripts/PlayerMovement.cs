using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Controllers")]
    public CharacterController controller;  // Reference to our CharacterController
    public float speed = 12f;

    [Header("Physics")]
    // gravity
    public float gravtiy = -9.8f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;    // Want to control what this object checks for

    Vector3 velocity;
    bool isGrounded;    // bool set default to false

    // Update is called once per frame
    void Update()
    {
        // This is creating a sphere based on the ground check, then use the ground distance as the radius, and the layermask will be groundmask.
        // If it collides with anything that is in our groundMask, then isGrounded is equal to true
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // if isGrounded and velocity is less than 0
        if (isGrounded && velocity.y < 0)    
        {
            // This forces our player on to the ground and resets our velocity
            velocity.y = -2f;
        }

        // These are the inputs that has already been mapped out to match the asdw buttons on the keyboard or controller.
        float x = Input.GetAxis("Horizontal");  
        float z = Input.GetAxis("Vertical");

        // #1 This takes the direction the player is facing and then goes to the right, multiply this by x
        // #2 This takes the direction the player is facing and then goes foward, multiply this by z
        Vector3 move = transform.right * x + transform.forward * z;

        // A function on this Character Controller that takes in a vector3 and set it in motion
        controller.Move(move * speed * Time.deltaTime); // Time.deltaTime makes it framerate independent

        // This checks if the player is grounded and if the space bar is pressed
        if(Input.GetButtonDown("Jump") && isGrounded) 
        {
            // Then if everything is true, we can jump by setting our velocity to the formula below and the player will move in the direction of the velocity in the y direction.
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravtiy);
        }

        // This is the velocity of gravity
        velocity.y += gravtiy * Time.deltaTime;

        // The formula for delta y (change in y) is (.5)(g)(t ^ 2). This is physics of free fall. 
        controller.Move(velocity * Time.deltaTime);  
    }
}
