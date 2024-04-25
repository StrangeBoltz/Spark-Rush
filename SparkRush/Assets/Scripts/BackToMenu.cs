using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Call a function to load scene 0
            LoadScene(0);
        }
    }

    void LoadScene(int sceneIndex)
    {
        // Load the scene with the specified index
        SceneManager.LoadScene(sceneIndex);
    }
}
