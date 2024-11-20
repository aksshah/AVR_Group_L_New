using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CharacterSpawner : MonoBehaviour
{
    private ARPlaneManager arPlaneManager; // Reference to the AR Plane Manager
    public GameObject objectToSpawn;       // The 3D object to spawn on the detected plane
    private GameObject spawnedObject;      // The spawned object to be moved by touch
    private bool objectSpawned = false;    // To ensure we only spawn one object
    private bool showMessage = true;       // Controls whether to display the scanning message

    public static bool IsCharacterSpawned { get; private set; } // Static flag to check if the character has been spawned

    private void Awake()
    {
        // Get the ARPlaneManager component from the XR Origin GameObject
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

    private void OnEnable()
    {
        // Subscribe to the planesChanged event to detect new planes
        arPlaneManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe from the planesChanged event to avoid memory leaks
        arPlaneManager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        // Only spawn the object once, avoid redundant checks
        if (objectSpawned) return;

        // Check if there are any newly detected planes
        foreach (ARPlane plane in arPlaneManager.trackables)
        {
            // Check if the plane area is sufficient (adjust the area threshold as needed)
            if (plane.extents.x * plane.extents.y >= 0.2f) // 0.2 square meter threshold
            {
                // Spawn the object at the center of the first large enough plane
                Vector3 spawnPosition = plane.center;
                spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
                spawnedObject.tag = "ControllableObject";  // Tag the spawned object
                Debug.Log("Object Spawned and tagged with: " + spawnedObject.tag);

                // Mark as spawned to prevent further spawning and hide the message
                objectSpawned = true;
                showMessage = false;

                // Set the static flag to notify other scripts that the character has been spawned
                IsCharacterSpawned = true;

                // Stop further checking once the object is spawned
                break;
            }
        }
    }

    private void OnGUI()
    {
        // Show the message only if the object hasn't been spawned yet
        if (showMessage)
        {
            // Set font size and style
            GUIStyle messageStyle = new GUIStyle();
            messageStyle.fontSize = 32;
            messageStyle.alignment = TextAnchor.MiddleCenter;
            messageStyle.normal.textColor = Color.white;

            // Display the message in the center of the screen
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 25, 300, 50), "Move your device to scan the surface", messageStyle);
        }
    }
}