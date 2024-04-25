using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private int highScore = 0;
    private float gameTimer = 60f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject loseScreen;
    public TextMeshProUGUI loseScreenScoreText;
    public TextMeshProUGUI loseScreenHighScoreText;
    public TextMeshProUGUI highscoreText;
    public bool gameEnded;
    public AudioClip loseSound;
    private AudioSource audioSource;
    public int scoreToUnlockLevel = 100; // Define the score threshold to unlock the level
    public Button nextLevelButton; // Reference to the button to proceed to the next level
    public GameObject lockImage; // Reference to the lock image GameObject


    // New variable to store level-specific high scores
    private int[] levelHighScores;

    void Start()
    {
        // Load all level high scores from PlayerPrefs
        levelHighScores = new int[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < levelHighScores.Length; i++)
        {
            levelHighScores[i] = PlayerPrefs.GetInt("HighScore_Level_" + i, 0);
        }

        // Load the high score for the current level
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        highScore = levelHighScores[currentLevelIndex];
        UpdateHighScoreUI();

        // Deactivate the lose screen at the start of the game
        if (loseScreen != null)
            loseScreen.SetActive(false);

        // Get the AudioSource component attached to the ScoreManager object
        audioSource = GetComponent<AudioSource>();

        // Deactivate the next level button initially
        if (nextLevelButton != null)
            nextLevelButton.gameObject.SetActive(false);
    }

    void Update()
    {
        // Update the timer
        gameTimer -= Time.deltaTime;
        UpdateTimerUI();

        // Check if the timer has reached 0
        if (gameTimer <= 0f && !gameEnded)
        {
            // Timer has ended, show the lose screen
            ShowLoseScreen();
            // Set gameEnded flag to true
            gameEnded = true;
        }

        // Check if the game has ended and the Enter key is pressed
        if (gameEnded && Input.GetKeyDown(KeyCode.Return))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Check if the high score meets the threshold to unlock the level
        if (highScore >= scoreToUnlockLevel)
        {
            if (nextLevelButton != null)
            {
                nextLevelButton.gameObject.SetActive(true); // Activate the next level button
                lockImage.SetActive(false); // Deactivate the lock image
            }
        }
        else
        {
            if (nextLevelButton != null)
            {
                nextLevelButton.gameObject.SetActive(false); // Deactivate the next level button
                lockImage.SetActive(true); // Activate the lock image
            }
        }
    }


    // Method to increase the score
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        // Check if the new score is higher than the high score for the current level
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        if (score > levelHighScores[currentLevelIndex])
        {
            levelHighScores[currentLevelIndex] = score;
            PlayerPrefs.SetInt("HighScore_Level_" + currentLevelIndex, score);
            // Save the new high score for the current level
            highScore = score; // Update the current high score
            UpdateHighScoreUI(); // Update the UI
        }
    }

    // Method to update the score UI
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = " " + score.ToString();
        }
    }

    // Method to update the timer UI
    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int seconds = Mathf.FloorToInt(gameTimer % 60);
            timerText.text = " " + seconds.ToString("00");
        }
    }

    // Method to update the high score UI
    void UpdateHighScoreUI()
    {
        if (highscoreText != null)
        {
            highscoreText.text = " " + highScore.ToString();
        }
    }

    // Method to show the lose screen
    void ShowLoseScreen()
    {
        // Activate the lose screen game object
        if (loseScreen != null)
            loseScreen.SetActive(true);

        // Update the lose screen score text
        if (loseScreenScoreText != null)
        {
            loseScreenScoreText.text = "Final Score: " + score.ToString();
        }

        // Update the lose screen high score text
        if (loseScreenHighScoreText != null)
        {
            loseScreenHighScoreText.text = "High Score: " + highScore.ToString();
        }

        // Play the lose sound effect
        if (loseSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(loseSound);
        }
    }

    // Method to unlock the next level
    public void ProceedToNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more levels available.");
            // You can handle this case differently, like showing a message or returning to the main menu.
        }
    }
}
