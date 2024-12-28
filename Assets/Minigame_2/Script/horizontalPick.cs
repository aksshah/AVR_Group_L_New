using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class HorizontalScroller : MonoBehaviour
{
    [SerializeField] private ARRaycastManager arRaycastManager; // Reference to AR Raycast Manager
    [SerializeField] private GameObject[] objectsToPlace; // Array of objects to place
    [SerializeField] private RectTransform scrollerContent; // Horizontal scroll bar content
    [SerializeField] private GameObject prefabButton; // Prefab button for each object
    [SerializeField] private Camera arCamera; // AR Camera

    private int selectedIndex = -1; // Index of the selected object
    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    private void Start()
    {
        InitializeScroller();
    }

    private void Update()
    {
        // Check for touch input and place the selected object
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && selectedIndex >= 0)
        {
            PlaceObject(Input.GetTouch(0).position);
        }
    }

    // Initialize horizontal scroll bar with buttons for each object
    private void InitializeScroller()
    {
        for (int i = 0; i < objectsToPlace.Length; i++)
        {
            int index = i; // Capture index for button click
            GameObject button = Instantiate(prefabButton, scrollerContent);
            button.GetComponentInChildren<Text>().text = objectsToPlace[i].name; // Set button text
            button.GetComponent<Button>().onClick.AddListener(() => OnObjectSelected(index));
        }
    }

    // Handle object selection
    private void OnObjectSelected(int index)
    {
        selectedIndex = index;
        Debug.Log($"Selected object: {objectsToPlace[index].name}");
    }

    // Place the selected object in the AR scene
    private void PlaceObject(Vector2 screenPosition)
    {
        if (arRaycastManager.Raycast(screenPosition, raycastHits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = raycastHits[0].pose; // Get the position and rotation of the hit
            Instantiate(objectsToPlace[selectedIndex], hitPose.position, hitPose.rotation);
        }
    }
}
