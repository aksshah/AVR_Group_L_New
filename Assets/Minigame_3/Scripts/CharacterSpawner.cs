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
        // Check if there are any newly detected planes and the object hasn't been spawned yet
        if (!objectSpawned && args.added.Count > 0)
        {
            // Get the first plane detected
            ARPlane detectedPlane = args.added[0];

            // Spawn the object at the plane's center
            Vector3 spawnPosition = detectedPlane.center;
            spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            spawnedObject.tag = "ControllableObject";  // Tag the spawned object
            Debug.Log("Object Spawned and tagged with: " + spawnedObject.tag);

            // Mark as spawned to prevent further spawning
            objectSpawned = true;
        }
    }
}
