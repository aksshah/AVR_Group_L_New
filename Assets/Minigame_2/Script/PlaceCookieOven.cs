using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BakingManager : MonoBehaviour
{
    public GameObject plate;
    public Transform ovenPosition;
    public TMP_Text timerText;
    public GameObject popUpUI;
    public Button startBakingButton;
    public Button placePlateButton;
    public Button newMiniGameButton;
    public Button playAgainButton;

    private float bakingTime = 10f;
    private bool isBaking = false;

    void Start()
    {
        timerText.text = "Timer: 00:00";
        popUpUI.SetActive(false);
        startBakingButton.interactable = false;
        startBakingButton.onClick.AddListener(StartBaking);
        placePlateButton.onClick.AddListener(PlacePlateInOven);
        newMiniGameButton.onClick.AddListener(StartNewMiniGame);
        playAgainButton.onClick.AddListener(PlayAgain);

        UpdateUI(); // Update UI based on saved state
    }

    void Update()
    {
        if (isBaking)
        {
            UpdateTimer();
        }
    }

    public void PlacePlateInOven()
    {
        plate.transform.position = ovenPosition.position;
        startBakingButton.interactable = true;
        placePlateButton.interactable = false;
    }

    public void StartBaking()
    {
        isBaking = true;
        startBakingButton.interactable = false;
    }

    private void UpdateTimer()
    {
        if (bakingTime > 0)
        {
            bakingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(bakingTime / 60);
            int seconds = Mathf.FloorToInt(bakingTime % 60);
            timerText.text = $"Timer: {minutes:00}:{seconds:00}";
        }
        else
        {
            isBaking = false;
            EndBaking();
        }
    }

    private void EndBaking()
    {
        timerText.text = "Timer: 00:00";
        popUpUI.SetActive(true);

        PlayerPrefs.SetInt("MiniGame2Completed", 1);
        PlayerPrefs.Save();
        Debug.Log("Mini-Game 2 completed! State saved.");
    }

    public bool IsMiniGame2Completed()
    {
        return PlayerPrefs.GetInt("MiniGame2Completed", 0) == 1;
    }

    public void ResetMiniGame2Completion()
    {
        PlayerPrefs.DeleteKey("MiniGame2Completed");
        Debug.Log("Mini-Game 2 completion state reset.");
    }

    void UpdateUI()
    {
        if (IsMiniGame2Completed())
        {
            newMiniGameButton.interactable = true;
        }
        else
        {
            newMiniGameButton.interactable = false;
        }
    }

    public void StartNewMiniGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("NavigationScene");
    }

    public void PlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGameScene1");
    }
}
