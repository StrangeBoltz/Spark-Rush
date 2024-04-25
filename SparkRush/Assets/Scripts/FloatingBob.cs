using UnityEngine;

public class FloatinBob : MonoBehaviour
{
    public float floatSpeed = 1.0f; // Speed of floating movement
    public float floatRange = 1.0f; // Range of floating movement

    private float originalY; // Original Y position

    void Start()
    {
        originalY = transform.position.y; // Store the original Y position
    }

    void Update()
    {
        // Calculate the new Y position based on sine wave
        float newY = originalY + Mathf.Sin(Time.time * floatSpeed) * floatRange;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
