using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCycle : MonoBehaviour
{
    public AudioClip[] audioTracks;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Check if the audio source and array are properly set up
        if (audioSource == null)
        {
            Debug.LogError("Audio Source component not found!");
            return;
        }

        if (audioTracks.Length == 0)
        {
            Debug.LogError("No audio tracks assigned to the array!");
            return;
        }

        // Select a random audio track from the array
        int randomIndex = Random.Range(0, audioTracks.Length);
        AudioClip randomTrack = audioTracks[randomIndex];

        // Play the selected audio track
        audioSource.clip = randomTrack;
        audioSource.Play();
    }
}
