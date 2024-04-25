using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishMotion : MonoBehaviour
{
    public float squishSpeed = 1.0f; // Adjust this to control the speed of the squashing motion
    public float squishAmount = 0.1f; // Adjust this to control the amount of squashing

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Calculate the squish amount using a sine function for undulating motion
        float squish = Mathf.Sin(Time.time * squishSpeed) * squishAmount;

        // Apply the squish effect to the scale of the GameObject
        transform.localScale = originalScale + new Vector3(squish, -squish, squish);
    }
}
