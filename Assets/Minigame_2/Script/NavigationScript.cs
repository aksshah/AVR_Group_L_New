using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationMapManager : MonoBehaviour
{
    public Button[] houseButtons; // Buttons corresponding to each house

    private void Start()
    {
        // Initialize button states
        for (int i = 0; i < houseButtons.Length; i++)
        {
            int index = i; // Local copy to avoid closure issues

            // Check the completion status of the game
            bool isCompleted = PlayerPrefs.GetInt("MiniGame" + index + "Completed", 0) == 1;

            // Update button visuals based on completion status
            UpdateButtonState(houseButtons[i], isCompleted, index);

            // Assign a click listener to load the corresponding mini-game
            houseButtons[i].onClick.AddListener(() => LoadMiniGame(index));
        }
    }

    private void UpdateButtonState(Button button, bool isCompleted, int index)
    {
        Text buttonText = button.GetComponentInChildren<Text>();

        if (isCompleted)
        {
            // Mark as completed with a green tick
            buttonText.text = "Completed ✔️";
            buttonText.color = Color.green;
            button.interactable = true; // Allow replay
        }
        else
        {
            if (index == 0 || PlayerPrefs.GetInt("MiniGame" + (index - 1) + "Completed", 0) == 1)
            {
                // Enable the button if it's the first game or the previous game is completed
                buttonText.text = "Game " + (index + 1);
                button.interactable = true;
            }
            else
            {
                // Lock the button if the previous game is not completed
                buttonText.text = "Locked";
                buttonText.color = Color.red;
                button.interactable = false;
            }
        }
    }

    public void LoadMiniGame(int houseIndex)
    {
        Debug.Log("Loading Mini-Game for House: " + (houseIndex + 1));
        SceneManager.LoadScene("MiniGameScene" + (houseIndex + 1));
    }

    public void CompleteMiniGame(int gameIndex)
    {
        Debug.Log("Mini-Game " + (gameIndex + 1) + " Completed!");

        // Mark the game as completed in PlayerPrefs
        PlayerPrefs.SetInt("MiniGame" + gameIndex + "Completed", 1);
        PlayerPrefs.Save();

        // Return to the map screen
        SceneManager.LoadScene("NavigationScene");
    }
}
