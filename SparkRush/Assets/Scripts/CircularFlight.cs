using UnityEngine;

public class CircularFlight : MonoBehaviour
{
    public float minRotationSpeed = 20f;
    public float maxRotationSpeed = 40f;
    public float minCircleRadius = 3f;
    public float maxCircleRadius = 7f;
    public float minCircleSpeed = 1f;
    public float maxCircleSpeed = 3f;
    public float minUpDownSpeed = 0.5f;
    public float maxUpDownSpeed = 1.5f;
    public float minUpDownAmplitude = 1f;
    public float maxUpDownAmplitude = 3f;

    private float rotationSpeed;
    private float circleRadius;
    private float circleSpeed;
    private float upDownSpeed;
    private float upDownAmplitude;
    private float angle = 0f;
    private Vector3 initialPosition;

    void Start()
    {
        // Randomize the values within the specified ranges
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        circleRadius = Random.Range(minCircleRadius, maxCircleRadius);
        circleSpeed = Random.Range(minCircleSpeed, maxCircleSpeed);
        upDownSpeed = Random.Range(minUpDownSpeed, maxUpDownSpeed);
        upDownAmplitude = Random.Range(minUpDownAmplitude, maxUpDownAmplitude);

        // Store the initial position
        initialPosition = transform.position;
    }

    void Update()
    {
        MoveInCircles();
    }

    private void MoveInCircles()
    {
        // Calculate the position on the circle
        float x = circleRadius * Mathf.Cos(angle);
        float y = upDownAmplitude * Mathf.Sin(angle * upDownSpeed);
        float z = circleRadius * Mathf.Sin(angle);

        // Set the new position relative to the initial position
        Vector3 newPosition = initialPosition + new Vector3(x, y, z);
        transform.position = newPosition;

        // Orient the object about an axis of rotation (e.g., Vector3.up)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Update the angle for the next frame
        angle += circleSpeed * Time.deltaTime;
    }
}