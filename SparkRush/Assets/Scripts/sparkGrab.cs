using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkGrab : MonoBehaviour
{
    public int scoreValue = 10; // The value of each spark collected
    private ScoreManager scoreManager; // Reference to the ScoreManager
    public AudioClip grabSound; // Audio clip to play when spark is grabbed
    public GameObject particleEffect; // Particle effect to play when spark is grabbed

    void Start()
    {
        // Find the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play grab sound
            if (grabSound != null)
            {
                AudioSource.PlayClipAtPoint(grabSound, transform.position);
            }

            // Instantiate particle effect
            if (particleEffect != null)
            {
                Instantiate(particleEffect, transform.position, Quaternion.identity);
            }

            // Increase score using the ScoreManager
            if (scoreManager != null)
            {
                scoreManager.IncreaseScore(scoreValue);
            }

            // Destroy the spark
            Destroy(gameObject);
        }
    }
}
