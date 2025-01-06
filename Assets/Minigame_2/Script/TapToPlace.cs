using UnityEngine;
using UnityEngine.UI;  // <-- Add this line
using System.Collections.Generic;
using TMPro;

public class CookiePlacer : MonoBehaviour
{
    public GameObject cookiePrefab; // Reference to the cookie prefab
    public Transform plateTransform; // Reference to the plate where cookies will be placed
    public List<Vector3> cookiePositions = new List<Vector3>(); // Predefined positions for cookies
    private int currentCookieIndex = 0; // Tracks the current position index
    public float cookieHeight = 0.1f; // Height above the plate for cookie placement

    public TMP_Text cookieCounterText; // Reference to the UI Text for the cookie counter
    public GameObject bakePopup; // Reference to the Bake Popup UI panel
    public TMP_Text bakeTimerText; // Reference to the UI Text to show the timer countdown

    private bool isBaking = false; // To track if the baking has started
    private float bakingTime = 10f; // Time for baking (10 seconds)

    private bool isMaxCookiesPlaced = false; // Flag to track if max cookies are placed

    // Start is called before the first frame update
    void Start()
    {
        // Define 10 cookie positions in a 2x5 grid format
        cookiePositions.Add(new Vector3(-0.2f, 0, 0.2f)); // Position 1
        cookiePositions.Add(new Vector3(0, 0, 0.2f));    // Position 2
        cookiePositions.Add(new Vector3(0.2f, 0, 0.2f));  // Position 3
        cookiePositions.Add(new Vector3(-0.2f, 0, -0.2f)); // Position 4
        cookiePositions.Add(new Vector3(0, 0, -0.2f));    // Position 5
        cookiePositions.Add(new Vector3(0.2f, 0, -0.2f));  // Position 6
        cookiePositions.Add(new Vector3(-0.2f, 0, 0.2f)); // Position 7
        cookiePositions.Add(new Vector3(0, 0, 0.2f));    // Position 8
        cookiePositions.Add(new Vector3(0.2f, 0, 0.2f));  // Position 9
        cookiePositions.Add(new Vector3(-0.2f, 0, -0.2f)); // Position 10

        Debug.Log("6 cookie positions defined.");
        UpdateCookieCounter();
        bakePopup.SetActive(false); // Hide the popup initially
    }

    // Update is called once per frame
    void Update()
    {
        // Detect user tap or click, only if max cookies are not placed yet
        if (!isBaking && !isMaxCookiesPlaced && Input.GetMouseButtonDown(0)) // For touch, replace with Input.touchCount > 0
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the plate
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == plateTransform) // Ensure the plate was tapped
                {
                    PlaceCookie(hit.point);
                }
            }
        }

        // If baking is in progress, count down the timer
        if (isBaking)
        {
            bakingTime -= Time.deltaTime;
            bakeTimerText.text = $"Baking: {Mathf.Ceil(bakingTime)}s";
            if (bakingTime <= 0)
            {
                EndBaking();
            }
        }
    }

    // Method to place a cookie at the tapped position or at predefined positions
    public void PlaceCookie(Vector3 tapPosition)
    {
        if (currentCookieIndex < 10) // Max 10 cookies
        {
            // Place the cookie at the predefined position
            Vector3 position = plateTransform.position + cookiePositions[currentCookieIndex];
            position.y = plateTransform.position.y + cookieHeight; // Adjust cookie height above the plate
            GameObject newCookie = Instantiate(cookiePrefab, position, Quaternion.identity);

            // Optional: Parent the cookie to the plate for better organization
            newCookie.transform.parent = plateTransform;

            currentCookieIndex++; // Increment the index for the next placement
            UpdateCookieCounter();
            Debug.Log($"Cookie placed! Current cookies on plate: {currentCookieIndex}");

            // Check if 10 cookies have been placed
            if (currentCookieIndex >= 10)
            {
                isMaxCookiesPlaced = true;
                ShowBakePopup();
            }
        }
    }

    // Method to update the cookie counter on the UI
    private void UpdateCookieCounter()
    {
        cookieCounterText.text = $"Cookies: {currentCookieIndex}/10";
    }

    // Method to show the bake popup when 10 cookies are placed
    private void ShowBakePopup()
    {
        bakePopup.SetActive(true);
    }

    // Method to start baking when the user confirms

    public void StartBaking()
    {
        bakePopup.SetActive(false); // Hide the popup
        isBaking = true; // Start the baking process
        bakingTime = 10f; // Reset the timer
        Debug.Log("Baking started!");
    }

    // Method to end the baking process and finish the game
    private void EndBaking()
    {
        isBaking = false;
        Debug.Log("Baking finished!");
        // Optionally show a final message or reset the game here
        // For example:
        // Show a message or reset game
        // Time.timeScale = 0; // Pause game, or you can restart the level
    }

    // Method to cancel baking (optional, if you want a "Cancel" button in the popup)
    public void CancelBaking()
    {
        bakePopup.SetActive(false); // Hide the popup
        Debug.Log("Baking canceled!");
    }
}
