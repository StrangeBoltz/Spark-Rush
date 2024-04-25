using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    // Reference to the mesh renderer of the object
    private MeshRenderer meshRenderer;

    // Minimum and maximum transparency values
    public float minTransparency = 13f;
    public float maxTransparency = 55f;

    // Speed of transparency fluctuation
    public float speed = 1.0f;

    void Start()
    {
        // Get the mesh renderer component attached to the object
        meshRenderer = GetComponent<MeshRenderer>();

        // Make sure we have a mesh renderer attached
        if (meshRenderer == null)
        {
            Debug.LogError("TransparencyController: MeshRenderer component not found!");
            return;
        }
    }

    void Update()
    {
        // Calculate transparency based on Perlin noise and time
        float transparency = Mathf.Lerp(minTransparency, maxTransparency, Mathf.PerlinNoise(Time.time * speed, 0));

        // Apply the transparency to the material
        SetTransparency(transparency);
    }

    // Set transparency for the object
    void SetTransparency(float transparency)
    {
        // Make a copy of the current material
        Material material = new Material(meshRenderer.material);

        // Set the transparency level
        Color color = material.color;
        color.a = transparency / 255f; // Unity's Color alpha range is 0 to 1
        material.color = color;

        // Apply the modified material to the mesh renderer
        meshRenderer.material = material;
    }
}
