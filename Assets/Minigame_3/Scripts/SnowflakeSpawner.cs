using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SnowflakeSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;       // The object to spawn
    private GameObject spawnedObject;      // The spawned object
    private bool objectSpawned = false;    // To prevent multiple spawns
    private ScoreManager scoreManager; // Reference to ScoreManager

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>(); // Find ScoreManager in the scene
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene!");
        }
    }

    private void Update()
    {
        // Only spawn snowflakes if the character has been spawned and the object hasn't been spawned yet
        if (CharacterSpawner.IsCharacterSpawned && !objectSpawned && (scoreManager.GetScore() < 10))
        {
            SpawnSnowflake();
        }
    }

    public void SpawnSnowflake()
    {
        // Find all planes with the "DetectedPlane" tag
        ARPlane[] detectedPlanes = FindObjectsOfType<ARPlane>();

        foreach (ARPlane plane in detectedPlanes)
        {
            if (plane.CompareTag("DetectedPlane"))  // Check if the plane has the "DetectedPlane" tag
            {
                // Get the plane's center and size (bounds)
                Vector3 planeCenter = plane.center;
                Vector3 planeExtent = plane.size;

                // Generate random x and z coordinates within the plane's bounds
                float randomX = Random.Range(planeCenter.x - planeExtent.x / 2, planeCenter.x + planeExtent.x / 2);
                float randomZ = Random.Range(planeCenter.z - planeExtent.z / 2, planeCenter.z + planeExtent.z / 2);
                Vector3 randomPosition = new Vector3(randomX, planeCenter.y, randomZ);  // Use center y for correct height

                // Debug the random position
                Debug.Log("Random Position: " + randomPosition);

                // Instantiate the object at the random position on the plane
                spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
                spawnedObject.tag = "CollectableObject"; // Tag the spawned object as "CollectableObject"
                Debug.Log("Collectable Object Spawned at: " + randomPosition);

                // Notify the CharacterControl script to move towards the collectable object
                CharacterControl characterControl = FindObjectOfType<CharacterControl>();
                if (characterControl != null)
                {
                    // Pass the spawned collectable object to CharacterControl
                    characterControl.SetCollectableObject(spawnedObject);
                }

                // Mark that the object has been spawned
                objectSpawned = true;
                break; // Exit the loop after spawning the object on the first detected plane
            }
        }
    }
}