using UnityEngine;
using TMPro; // Use TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the UI TextMeshPro element
    private int currentScore = 0; // Tracks the player's current score
    public int maxScore = 10; // The maximum score (e.g., 10)
    public GameObject endScreenCanvas;
    public GameObject xrOrigin;

    private void Start()
    {
        // Initialize the score text display at the start
        UpdateScoreText();
    }

    // Call this method to increase the score and update the UI
    public void IncreaseScore()
    {
        if (currentScore < maxScore) // Ensures score doesn't exceed maxScore
        {
            currentScore++;
            UpdateScoreText();
        }
    }

    public int GetScore()
    {
        return currentScore; // Return the current score
    }

    // Updates the score text on the screen
    private void UpdateScoreText()
    {
        scoreText.text = currentScore + "/" + maxScore;

        if (currentScore == maxScore) {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over! All cookies collected!");

        // Show the end screen canvas
        if (endScreenCanvas != null)
        {
            endScreenCanvas.SetActive(true);
        }
        else
        {
            Debug.LogWarning("End Screen Canvas is not assigned!");
        }

        DisableARComponents();
    }

    private void DisableARComponents()
    {
        // Example: Disabling specific AR-related components
        var characterSpawner = xrOrigin.GetComponent<CharacterSpawner>();
        if (characterSpawner != null)
        {
            characterSpawner.enabled = false;
            Debug.Log("Disabled CharacterSpawner script.");
        }

        var characterControl = xrOrigin.GetComponent<CharacterControl>();
        if (characterControl != null)
        {
            characterControl.enabled = false;
            Debug.Log("Disabled CharacterControl script.");
        }

        var snowflakeSpawner = xrOrigin.GetComponent<SnowflakeSpawner>();
        if (snowflakeSpawner != null)
        {
            snowflakeSpawner.enabled = false;
            Debug.Log("Disabled SnowflakeSpawner script.");
        }

        enabled = false; // Disable this script to stop further updates.
    }
}
