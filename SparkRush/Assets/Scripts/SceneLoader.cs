using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Index of the scene to load
    public int sceneIndex;

    void Start()
    {
        // Find the button component attached to this GameObject
        Button button = GetComponent<Button>();

        // Add a listener for the button click event
        button.onClick.AddListener(LoadSceneOnClick);
    }

    void LoadSceneOnClick()
    {
        // Load the specified scene by index
        SceneManager.LoadScene(sceneIndex);
    }
}
