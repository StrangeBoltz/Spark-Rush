using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Speed of rotation in degrees per second
    public float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the Y axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
