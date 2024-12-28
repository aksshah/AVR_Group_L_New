using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Elementplacement : MonoBehaviour
{
    [SerializeField] private XROrigin _xrOrigin;
    [SerializeField] private ARPlaneManager _planeManager;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject _placementObject;

    private GameObject _instantiatedObject = null;

    private List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            bool collision = _raycastManager.Raycast(Input.GetTouch(0).position, _raycastHits, TrackableType.PlaneWithinPolygon);

            if (collision && isButtonPressed() == false)
            {
                _instantiatedObject = Instantiate(_placementObject, _raycastHits[0].pose.position, _raycastHits[0].pose.rotation);

                foreach (ARPlane plane in _planeManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
                _planeManager.enabled = false;
            }
        }
    }

    public bool isButtonPressed()
    {
        if (EventSystem.current.currentSelectedGameObject?.GetComponent<Button>() == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SwitchFurniture(GameObject Furniture)
    {
        _placementObject = Furniture;
    }
}
