using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshPro

public class RotateObject : MonoBehaviour
{
    public GameObject objectRotate; // The spoon object to rotate
    public Transform bowlCenter; // Center point of the bowl
    public float rotateSpeed = 50f; // Speed of rotation

    public GameObject popUp; // The pop-up UI element
    public GameObject nextStepButton; // Button to proceed to the next step
    public TMP_Text rotationCountTextAR; // Text on the AR screen to show rotation count
    public TMP_Text rotationCountTextPopUp; // Text on the pop-up to show rotation count

    private bool rotateStatus = false; // Whether the spoon is rotating
    private int rotationCount = 0; // Track the number of rotations
    private float totalRotation = 0f; // Track the total rotation angle

    void Start()
    {
        // Ensure the pop-up and next step button are hidden initially
        if (popUp != null) popUp.SetActive(false);
        if (nextStepButton != null) nextStepButton.SetActive(false);

        // Initialize rotation count text on AR screen and pop-up
        UpdateRotationTexts();
    }

    // Toggle rotation on and off
    public void ToggleRotation()
    {
        rotateStatus = !rotateStatus; // Toggle rotation status
    }

    void Update()
    {
        if (rotateStatus)
        {
            // Rotate the spoon around the bowl's center
            float rotationStep = rotateSpeed * Time.deltaTime;
            if (bowlCenter != null)
            {
                objectRotate.transform.RotateAround(bowlCenter.position, Vector3.up, rotationStep);
            }
            else
            {
                Debug.LogError("Bowl Center is not assigned in the Inspector!");
            }

            // Track the total rotation
            totalRotation += rotationStep;
            if (totalRotation >= 360f)
            {
                totalRotation -= 360f; // Reset the total rotation after completing one full circle
                rotationCount++;

                // Update the rotation count on the AR screen
                UpdateRotationTexts();

                // Stop rotation and show pop-up after four rotations
                if (rotationCount >= 4)
                {
                    rotateStatus = false;
                    ShowPopUp();
                }
            }
        }
    }

    // Update rotation count text on both AR screen and pop-up
    private void UpdateRotationTexts()
    {
        if (rotationCountTextAR != null)
            rotationCountTextAR.text = $"Rotations: {rotationCount}";

        if (rotationCountTextPopUp != null)
            rotationCountTextPopUp.text = $"Rotations Completed: {rotationCount}";
    }

    // Show the pop-up and enable the next step button
    private void ShowPopUp()
    {
        if (popUp != null)
        {
            popUp.SetActive(true);
        }
        if (nextStepButton != null)
        {
            nextStepButton.SetActive(true);
        }
    }

    // Reset the rotation count and hide the pop-up
    public void ResetMixing()
    {
        rotationCount = 0;
        totalRotation = 0f;
        UpdateRotationTexts();

        if (popUp != null)
            popUp.SetActive(false);

        if (nextStepButton != null)
            nextStepButton.SetActive(false);
    }

    // Proceed to the next step (e.g., next scene or activity)
    public void ProceedToNextStep()
    {
        // Load the next scene or perform another action
        SceneManager.LoadScene("BakeScene"); // Replace "Riddle" with the actual scene name
    }
}
