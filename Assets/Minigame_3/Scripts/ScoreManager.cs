using UnityEngine;
using TMPro; // Use TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the UI TextMeshPro element
    private int currentScore = 0; // Tracks the player's current score
    public int maxScore = 10; // The maximum score (e.g., 10)

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
    }
}
