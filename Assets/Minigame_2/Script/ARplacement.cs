using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject bowlPrefab;
    [SerializeField] private GameObject spoonPrefab;

    private GameObject bowlInstance;
    private GameObject spoonInstance;
    private ARRaycastManager raycastManager;
    private bool objectsPlaced = false;

    void Start()
    {
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (objectsPlaced || Input.touchCount == 0) return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObjects(Input.GetTouch(0).position);
        }
    }

    void PlaceObjects(Vector2 touchPosition)
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            if (!objectsPlaced)
            {
                bowlInstance = Instantiate(bowlPrefab, hitPose.position, hitPose.rotation);
                spoonInstance = Instantiate(spoonPrefab, hitPose.position + new Vector3(0.2f, 0, 0), hitPose.rotation);
                objectsPlaced = true;
            }
        }
    }
}
