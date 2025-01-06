using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    public GameObject quitPopup; // Reference to the popup UI
    public Button quitButton; // Button that shows the quit popup
    public Button stayButton; // Button to stay in the game
    public Button confirmQuitButton; // Button to confirm quit

    void Start()
    {
        // You don't need to add listeners in code, just link buttons via the Inspector
    }

    // Show the quit confirmation popup
    public void ShowQuitPopup()
    {
        quitPopup.SetActive(true);
    }

    // Close the popup and quit the game
    public void QuitApp()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Hide the popup and stay in the game
    public void HideQuitPopup()
    {
        quitPopup.SetActive(false);
    }
}
