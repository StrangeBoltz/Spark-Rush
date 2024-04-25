using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Boid
{
    public Vector3 position;
    public Vector3 velocity;
    public int type;
    public float wanderAngle;
    public Quaternion rotation;
    public GameObject boidMeshPrefab;
}

public class Boids3D_Simulator : MonoBehaviour
{
    public List<Boid> boids;
    public GameObject[] boidMeshPrefabs;
    public Vector3 bounds = Vector3.one;
    public float boundsScale = 10.0f;
    public float boundingForce = 1.0f;
    public int typeCount = 1;
    public float cellSize = 2.0f;
    public float drag = 0.1f;
    public float minSpeed = 1.0f;
    public float maxSpeed = 2.0f;
    public float cohesionRadius = 1.0f;
    public float separationRadius = 0.5f;
    public float alignmentRadius = 1.0f;
    public float cohesionForce = 1.0f;
    public float separationForce = 1.0f;
    public float alignmentForce = 1.0f;
    public float wanderRadius = 1.0f;
    public float wanderDistance = 2.0f;
    [Range(0.0f, 360.0f)]
    public float wanderAngleJitter = 45.0f;
    public float wanderForce = 1.0f;
    public int addBoidCount = 10;
    public float addBoidRandomSpeedScale = 1.0f;
    public bool debugBase;
    public bool debugText;
    public bool debugWander;
    public float debugRadius = 0.1f;
    public Color debugColour = Color.white;

    new Boids3D_Renderer renderer;

    void Awake()
    {
        boids = new List<Boid>();
        renderer = GetComponent<Boids3D_Renderer>();
    }

    void Start()
        {
        // Initialize boids and set their mesh prefabs based on their type
        for (int i = 0; i < boids.Count; i++)
        {
            Boid boid = boids[i];
            if (boidMeshPrefabs.Length > boid.type && boidMeshPrefabs[boid.type] != null)
            {
                boid.boidMeshPrefab = boidMeshPrefabs[boid.type]; // Assign the mesh prefab based on the boid type
            }
            boids[i] = boid;
        }
    }

    public void AddBoids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * boundsScale;
            Vector3 randomVelocity = Random.insideUnitSphere.normalized * Random.Range(minSpeed, maxSpeed);

            Boid boid = new Boid
            {
                position = randomPosition,
                velocity = randomVelocity,
                type = Random.Range(0, typeCount)
            };

            boids.Add(boid);
        }
    }

    public void RemoveBoids(int count)
    {
        int numToRemove = Mathf.Min(count, boids.Count);
        for (int i = 0; i < numToRemove; i++)
        {
            renderer.RemoveBoidMesh(i); // Remove the corresponding boidMesh
        }
        boids.RemoveRange(0, numToRemove);
    }

    void FixedUpdate()
    {
        float deltaTime = Time.fixedDeltaTime;
        Vector3 transformPosition = transform.position;
        Vector3 scaledBoundingExtents = bounds * (boundsScale / 2.0f);

        // Create a copy of the boids list
        List<Boid> boidsCopy = new List<Boid>(boids);

        // Create a new list to store the modified boids
        List<Boid> modifiedBoids = new List<Boid>();

        foreach (Boid boid in boidsCopy)
        {
            Vector3 force = Vector3.zero;
            force += GetBoundingForce(boid.position, transformPosition, scaledBoundingExtents, boundingForce);

            // Create a new Boid struct with modified properties
            Boid modifiedBoid = boid;
            modifiedBoid.velocity *= 1.0f - drag * deltaTime;
            modifiedBoid.velocity += force * deltaTime;

            float currentSpeed = modifiedBoid.velocity.magnitude;
            Vector3 currentDirection = modifiedBoid.velocity.normalized;

            if (currentSpeed < minSpeed)
            {
                modifiedBoid.velocity = currentDirection * minSpeed;
            }
            else if (currentSpeed > maxSpeed)
            {
                modifiedBoid.velocity = currentDirection * maxSpeed;
            }

            Vector3 wander = Wander(modifiedBoid.position, modifiedBoid.velocity.normalized, ref modifiedBoid.wanderAngle, wanderDistance, wanderRadius, wanderAngleJitter) * wanderForce;

            modifiedBoid.position += modifiedBoid.velocity * deltaTime;

            // Update the rotation of the boid based on its velocity
            Quaternion rotation = Quaternion.LookRotation(modifiedBoid.velocity, Vector3.up);
            modifiedBoid.rotation = rotation;

            modifiedBoids.Add(modifiedBoid);
        }

        // Update the original boids list with the modified boids
        boids = modifiedBoids;
    }

    Vector3 GetBoundingForce(Vector3 position, Vector3 origin, Vector3 extents, float forceScale)
    {
        Vector3 lowerBound = origin - extents;
        Vector3 upperBound = origin + extents;
        Vector3 force = Vector3.zero;

        for (int i = 0; i < 3; ++i)
        {
            if (position[i] < lowerBound[i])
            {
                force[i] = forceScale;
            }
            else if (position[i] > upperBound[i])
            {
                force[i] = -forceScale;
            }
        }

        return force;
    }

    Vector3 Wander(Vector3 position, Vector3 forward, ref float currentWanderAngle, float wanderDistance, float wanderRadius, float wanderAngleJitter)
    {
        currentWanderAngle += Random.Range(-wanderAngleJitter, wanderAngleJitter);
        Vector3 circleOffset = Quaternion.AngleAxis(currentWanderAngle, Vector3.up) * forward * wanderRadius;
        Vector3 wanderTarget = position + forward * wanderDistance + circleOffset;
        Vector3 offsetToTarget = wanderTarget - position;
        return offsetToTarget;
    }

    void Update()
    {
    //    if (Input.GetKeyDown(KeyCode.Space))
     //   {
     //       AddBoids(addBoidCount);
    //    }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, bounds * boundsScale);

        if (!Application.isPlaying)
        {
            return;
        }

        Color colour = debugColour;
        Gizmos.color = colour;

        // Draw boids using their mesh prefabs
        foreach (Boid boid in boids)
        {
            if (boid.boidMeshPrefab != null)
            {
                Gizmos.DrawMesh(boid.boidMeshPrefab.GetComponent<MeshFilter>().sharedMesh, boid.position, boid.rotation);
            }
        }

        foreach (Boid boid in boids)
        {
            if (boid.velocity != Vector3.zero)
            {
                if (debugBase)
                {
                    Vector3 boidForward = boid.velocity.normalized;
                    Gizmos.DrawRay(boid.position, boidForward);

                    if (debugWander)
                    {
                        colour = Color.green;
                        colour.a = debugColour.a;
                        Gizmos.color = colour;
                        Gizmos.DrawWireSphere(boid.position + (boidForward * wanderDistance), wanderRadius);
                    }
                }
            }
        }
    }
}
