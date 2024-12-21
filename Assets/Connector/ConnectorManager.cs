using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class ConnectorManager : MonoBehaviour
{
    private ARTrackedImageManager imageManager;

    // Flags to check if mini-games are completed
    private static bool image1Tracked = false;
    private static bool image2Tracked = false;
    private static bool image3Tracked = false;

    // References to hint canvases
    public GameObject HintCanvas1;
    public GameObject HintCanvas2;
    public GameObject HintCanvas3;

    private void Start()
    {
        // Keep this object alive across scenes
        DontDestroyOnLoad(gameObject);

        // Find the ARTrackedImageManager in the scene
        imageManager = FindObjectOfType<ARTrackedImageManager>();

        // Subscribe to image tracking events
        if (imageManager != null)
        {
            imageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }

        // Show the first hint initially
        ShowHint(1);
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            HandleTrackedImage(trackedImage);
        }

        foreach (var trackedImage in args.updated)
        {
            HandleTrackedImage(trackedImage);
        }
    }

    private void HandleTrackedImage(ARTrackedImage trackedImage)
{
    // Log that this function was called
    Debug.Log("HandleTrackedImage() called");

    // Get the name of the detected image
    string imageName = trackedImage.referenceImage.name;
    Debug.Log($"Detected image name: {imageName}");

    // Check which image was tracked and handle it
    if (imageName == "Image1" && !image1Tracked)
    {
        image1Tracked = true;
        Debug.Log("Image1 tracked, loading MiniGame1Scene...");
        LoadMiniGame("MiniGame1Scene");
    }
    else if (imageName == "Image2" && !image2Tracked)
    {
        image2Tracked = true;
        Debug.Log("Image2 tracked, loading MiniGame2Scene...");
        LoadMiniGame("MiniGame2Scene");
    }
    else if (imageName == "Image3" && !image3Tracked)
    {
        image3Tracked = true;
        Debug.Log("Image3 tracked, loading MiniGame3Scene...");
        LoadMiniGame("MiniGame3Scene");
    }
}


    private void LoadMiniGame(string sceneName)
    {
        // Load the specified mini-game scene
        SceneManager.LoadScene(sceneName);
    }

    private void ShowHint(int hintNumber)
    {
        // Disable all hint canvases
        HintCanvas1.SetActive(false);
        HintCanvas2.SetActive(false);
        HintCanvas3.SetActive(false);

        // Enable the specific hint canvas
        if (hintNumber == 1)
        {
            HintCanvas1.SetActive(true);
        }
        else if (hintNumber == 2)
        {
            HintCanvas2.SetActive(true);
        }
        else if (hintNumber == 3)
        {
            HintCanvas3.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from image tracking events
        if (imageManager != null)
        {
            imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }
}
