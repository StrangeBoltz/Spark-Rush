using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids3D_Renderer : MonoBehaviour
{
    Boids3D_Simulator simulator;
    public List<GameObject> boidMeshes = new List<GameObject>();
    public GameObject[] boidMeshPrefabs; // Array of Boid Mesh Prefabs
    GameManager gameManager; // Reference to the game manager

    void Start()
    {
        simulator = GetComponent<Boids3D_Simulator>();
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene

        // Instantiate mesh instances for each boid
        foreach (Boid boid in simulator.boids)
        {
            Vector3 spawnPosition = gameManager.GetRandomSpawnPosition(); // Get random spawn position
            GameObject boidMeshPrefab = boidMeshPrefabs[boid.type]; // Assign the mesh prefab based on the boid type
            GameObject boidMesh = Instantiate(boidMeshPrefab, spawnPosition, Quaternion.identity);
            boidMeshes.Add(boidMesh);
        }
    }
    public void RemoveBoidMesh(int boidIndex)
    {
        if (boidIndex >= 0 && boidIndex < boidMeshes.Count)
        {
            Destroy(boidMeshes[boidIndex]);
            boidMeshes.RemoveAt(boidIndex);
        }
    }

    void Update()
    {
        int boidCount = simulator.boids.Count;

        // Ensure the boidMeshes list matches the boid count
        if (boidMeshes.Count != boidCount)
        {
            // Add or remove mesh instances as needed
            while (boidMeshes.Count < boidCount)
            {
                Vector3 spawnPosition = gameManager.GetRandomSpawnPosition();
                GameObject boidMeshPrefab = boidMeshPrefabs[simulator.boids[boidMeshes.Count].type];
                GameObject boidMesh = Instantiate(boidMeshPrefab, spawnPosition, Quaternion.identity);
                boidMeshes.Add(boidMesh);
            }

            while (boidMeshes.Count > boidCount)
            {
                Destroy(boidMeshes[boidMeshes.Count - 1]);
                boidMeshes.RemoveAt(boidMeshes.Count - 1);
            }
        }

        for (int i = 0; i < boidCount; i++)
        {
            Boid boid = simulator.boids[i];
            GameObject boidMesh = boidMeshes[i];

            // Add a null check here
            if (boidMesh != null)
            {
                // Update position
                boidMesh.transform.position = boid.position;

                // Update rotation
                Quaternion rotation = Quaternion.LookRotation(boid.velocity, Vector3.up);
                boidMesh.transform.rotation = rotation;
            }
        }
    }
}
