using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARSpoonBowl : MonoBehaviour
{
    [SerializeField] private GameObject spoonPrefab; // Spoon Prefab
    [SerializeField] private GameObject bowlPrefab; // Bowl Prefab
    [SerializeField] private ARRaycastManager arRaycastManager; // AR Raycast Manager
    [SerializeField] private ARPlaneManager arPlaneManager; // AR Plane Manager

    private GameObject spoonInstance;
    private GameObject bowlInstance;
    private bool isSpoonPlaced = false; // Track if spoon is placed
    private bool isBowlPlaced = false; // Track if bowl is placed

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        // Check for user touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                PlaceObjects(touch.position);
            }
        }
    }

    private void PlaceObjects(Vector2 screenPosition)
    {
        // Raycast to detect AR planes
        if (arRaycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            // Place the bowl if not placed
            if (!isBowlPlaced)
            {
                bowlInstance = Instantiate(bowlPrefab, hitPose.position, hitPose.rotation);
                isBowlPlaced = true;
                Debug.Log("Bowl placed!");
            }
            // Place the spoon if bowl is already placed
            else if (!isSpoonPlaced)
            {
                // Offset the spoon slightly next to the bowl
                Vector3 spoonPosition = hitPose.position + new Vector3(0.2f, 0, 0);
                spoonInstance = Instantiate(spoonPrefab, spoonPosition, hitPose.rotation);
                isSpoonPlaced = true;
                Debug.Log("Spoon placed!");
            }
        }
    }

    // Public method to rotate spoon (can be called from UI buttons)
    public void RotateSpoonleft()
    {
        if (spoonInstance != null)
        {
            spoonInstance.transform.Rotate(Vector3.up * 50 * Time.deltaTime);
        }
    }

    public void RotateSpoonCounterright()
    {
        if (spoonInstance != null)
        {
            spoonInstance.transform.Rotate(Vector3.down * 50 * Time.deltaTime);
        }
    }
}
