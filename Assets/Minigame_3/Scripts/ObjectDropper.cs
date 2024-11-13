using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectDropper : MonoBehaviour
{
    private ARPlaneManager arPlaneManager;
    public GameObject objectToDrop;
    public float dropHeight = 2f;
    public float dropInterval = 1f;

    private bool hasStartedDropping = false;

    private void Awake()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        if (arPlaneManager == null)
        {
            Debug.LogError("AR Plane Manager component not found on XR Origin!");
        }
    }

    private void OnEnable()
    {
        if (arPlaneManager != null)
        {
            arPlaneManager.planesChanged += OnPlanesChanged;
        }
    }

    private void OnDisable()
    {
        if (arPlaneManager != null)
        {
            arPlaneManager.planesChanged -= OnPlanesChanged;
        }
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (!hasStartedDropping && args.added.Count > 0)
        {
            hasStartedDropping = true;
            Debug.Log("Plane detected. Starting object drop.");
            StartCoroutine(DropObjects(args.added[0]));
        }
    }

    private IEnumerator DropObjects(ARPlane plane)
    {
        // Wait a moment to make sure the AR system is initialized before dropping objects
        yield return new WaitForSeconds(0.5f);

        while (hasStartedDropping)
        {
            // Ensure we are getting a valid position in world space
            Vector2 planeSize = plane.size;
            Vector3 planeCenter = plane.center;

            // Calculate a random position within the plane bounds
            Vector3 dropPosition = new Vector3(
                planeCenter.x + Random.Range(-planeSize.x / 2, planeSize.x / 2),
                planeCenter.y + dropHeight, // Add the dropHeight to drop the object above the plane
                planeCenter.z + Random.Range(-planeSize.y / 2, planeSize.y / 2)
            );

            Debug.Log("Dropping object at: " + dropPosition);

            // Instantiate the object at the calculated position
            Instantiate(objectToDrop, dropPosition, Quaternion.identity);

            // Wait for the specified interval before dropping the next object
            yield return new WaitForSeconds(dropInterval);
        }
    }

}
