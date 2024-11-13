using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Camera mainCamera; // The camera used for raycasting
    private GameObject controllableObject; // The object that will be controlled

    private void Start()
    {
        // Get the main camera for raycasting
        mainCamera = Camera.main;
    }

    private void Update()
    {

        // Check if the controllable object is assigned
        if (controllableObject == null)
        {
            // Find the controllable object in the scene (it should have the "ControllableObject" tag)
            controllableObject = GameObject.FindGameObjectWithTag("ControllableObject");

            // Debugging: Ensure that the object is found
            if (controllableObject != null)
            {
                Debug.Log("Controllable object found: " + controllableObject.name);
            }
            else
            {
                Debug.LogWarning("Controllable object not found yet. Waiting for spawn.");
            }
        }

        // Ensure there's a touch to process
        if (Input.touchCount > 0 && controllableObject != null)
        {
            Touch touch = Input.GetTouch(0);  // Get the first touch

            // Log touch position for debugging
            Debug.Log("Touch detected at position: " + touch.position);

            // Convert the touch position to a ray in world space
            Ray ray = mainCamera.ScreenPointToRay(touch.position);

            // Perform the raycast
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Log the raycast hit position for debugging
                Debug.Log("Ray hit at position: " + hit.point);

                // If the ray hits the plane, move the object to that point
                if (hit.transform != null && hit.transform.CompareTag("ControllableObject"))
                {
                    // Move the object to the touch point
                    controllableObject.transform.position = hit.point;

                    // Optionally, you can ensure the object rotates to face the touch position (if desired)
                    Vector3 lookDirection = hit.point - controllableObject.transform.position;
                    controllableObject.transform.rotation = Quaternion.LookRotation(lookDirection);
                }
            }
            else
            {
                // Log that the ray didn't hit anything
                Debug.Log("Ray did not hit anything.");
            }
        }
        else
        {
            // Log if no touches are detected
            if (Input.touchCount == 0)
            {
                Debug.Log("No touch detected.");
            }
        }
    }
}
