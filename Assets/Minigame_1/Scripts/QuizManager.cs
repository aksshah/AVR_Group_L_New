using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class QuestionData
{
    public string questionText; // The question text
    public Sprite[] optionImages; // Array of 4 option images
    public int correctAnswerIndex; // Index of the correct answer (0-3)
}

public class QuizManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text questionText;
    public List<Button> optionButtons;
    public Image progressIndicator;
    public List<Sprite> progressImages; // Updated to use Sprite
    public List<Sprite> ribbonSprites; // Ribbon sprites (e.g., "Question 1")
    public Image ribbonImageComponent; // This is the Image component on the Canvas for the ribbon

    [Header("Quiz Data")]
    [SerializeField]
    private List<QuestionData> questionsData; // List of all question data

    [Header("End Screen")]
    public GameObject itemUnlockedScreen; // "Item Unlocked" screen

    private int currentQuestionIndex = 0;

    void Start()
    {
        Debug.Log("QuizManager script is active and running."); // Debug log to check if the script is running
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        // Get the current question data
        QuestionData currentQuestion = questionsData[currentQuestionIndex];

        // Update question text
        questionText.text = currentQuestion.questionText;

        // Update ribbon image
        ribbonImageComponent.sprite = ribbonSprites[currentQuestionIndex];

        // Update progress bar
        progressIndicator.sprite = progressImages[currentQuestionIndex];

        // Update option button images
        foreach (var button in optionButtons)
        {
            button.image.sprite = null; // Clear previous images
        }
        
        // Debug: Check if the current question has 4 option images
            if (currentQuestion.optionImages.Length != optionButtons.Count)
            {
                Debug.LogError($"Question {currentQuestionIndex + 1} does not have 4 option images! Check the Inspector.");
                return; // Exit the method if the data is invalid
            }
        Debug.Log($"Displaying question {currentQuestionIndex + 1}. Options: {questionsData[currentQuestionIndex].optionImages.Length}");
        Debug.Log($"Displaying Question {currentQuestionIndex + 1}");
        Debug.Log($"Question Text: {currentQuestion.questionText}");
        for (int i = 0; i < currentQuestion.optionImages.Length; i++)
        {
            Debug.Log($"Option {i + 1} Image: {currentQuestion.optionImages[i]?.name}");
        }


        for (int i = 0; i < optionButtons.Count; i++)
        {
            if (questionsData[currentQuestionIndex].optionImages[i] != null)
            {
                optionButtons[i].image.sprite = questionsData[currentQuestionIndex].optionImages[i];
                //optionButtons[i].image.SetNativeSize(); // Adjust image size if needed
                Debug.Log($"Assigned Image {questionsData[currentQuestionIndex].optionImages[i].name} to Button {i + 1}");
            }
            else
            {
                Debug.LogError($"Option {i + 1} for Question {currentQuestionIndex + 1} has a null image! Check the Inspector.");
            }

            int optionIndex = i; // Capture the current index
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => CheckAnswer(optionIndex));
        }
    }

    void CheckAnswer(int selectedOption)
    {
        // Ensure currentQuestionIndex is within bounds
        if (currentQuestionIndex < questionsData.Count)
        {
            if (selectedOption == questionsData[currentQuestionIndex].correctAnswerIndex)
            {
                // Correct answer logic
                currentQuestionIndex++;

                if (currentQuestionIndex < questionsData.Count)
                {
                    DisplayQuestion();
                }
                else
                {
                    EndQuiz(); // No more questions, end the quiz
                }
            }
            else
            {
                // Incorrect answer logic
                Debug.Log("Wrong Answer");
            }
        }
        else
        {
            Debug.LogError("currentQuestionIndex is out of bounds!");
        }
    }


    void EndQuiz()
    {
        // Show the "Item Unlocked" screen
        itemUnlockedScreen.SetActive(true);
    }
}
