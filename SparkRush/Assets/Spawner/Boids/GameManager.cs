using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public Vector2Int startResolution = new Vector2Int(1920, 1080); 
    public int boindsOnStart = 100; 
    public Vector3 spawnAreaCenter = Vector3.zero;
    public Vector3 spawnAreaSize = new Vector3(10f, 10f, 10f); // Adjust as needed

    // Add a public variable to control the target FPS
    public int targetFPS = 60;

    void Start()
    {
        // Only do this if the game is launched on Desktop.
       // if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
       // {
       //     Screen.SetResolution(startResolution.x, startResolution.y, false);
       // }

        FindAnyObjectByType<Boids3D_Simulator>().AddBoids(boindsOnStart);
        
        // Set the target frame rate
        Application.targetFrameRate = targetFPS;
    }

    void Update()
    {

    }

    // Helper method to find any object with a given component type T
    new T FindAnyObjectByType<T>() where T : Component
    {
        T[] components = FindObjectsOfType<T>();
        if (components.Length > 0)
        {
            return components[0];
        }
        else
        {
            Debug.LogError("Cannot find object with component type: " + typeof(T).Name);
            return null;
        }
    }

    // Method to generate random spawn positions within a specified area
    public Vector3 GetRandomSpawnPosition()
    {
        Vector3 minBounds = spawnAreaCenter - spawnAreaSize / 2;
        Vector3 maxBounds = spawnAreaCenter + spawnAreaSize / 2;
        return new Vector3(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y), Random.Range(minBounds.z, maxBounds.z));
    }

    // Draw Gizmos for the spawn area
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}
