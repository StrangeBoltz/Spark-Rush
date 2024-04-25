using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    public float speed = 20.0f;
    public float turnSpeed = 55.0f;

    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;
    public ScoreManager gm;
    public AudioSource audioSource; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>(); // Get reference to AudioSource
    }

    void FixedUpdate()
    {
                // Check if the game has ended, and destroy the player game object if so
        if (gm.gameEnded)
        {
            Destroy(gameObject);
            return;
        }
        // Get input from the player
        float forwardInput = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f; // Use space bar for forward movement
        verticalInput = Input.GetAxis("Vertical"); // Use up and down arrow keys for vertical movement
        horizontalInput = Input.GetAxis("Horizontal"); // Use left and right arrow keys for rotation

        // Calculate the movement and rotation
        Vector3 movement = transform.forward * speed * forwardInput + Vector3.up * verticalInput * speed;
        Vector3 rotation = Vector3.up * turnSpeed * horizontalInput;

        // Apply the movement and rotation using Rigidbody.AddForce and Rigidbody.AddTorque
        rb.AddForce(movement * Time.deltaTime, ForceMode.VelocityChange);
        rb.AddTorque(rotation * Time.deltaTime, ForceMode.VelocityChange);
    }
}