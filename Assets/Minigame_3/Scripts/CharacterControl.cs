using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject controllableObject;
    private Animator animator;
    public string planeTag = "DetectedPlane";
    private Vector3 targetPosition;
    private bool isMoving = false;
    public float moveSpeed = 0.1f;
    private GameObject collectableObject; // To store the spawned collectable object (snowflake)

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Check if the controllable object is assigned
        if (controllableObject == null)
        {
            controllableObject = GameObject.FindGameObjectWithTag("ControllableObject");

            if (controllableObject != null)
            {
                Debug.Log("Controllable object found: " + controllableObject.name);
                animator = controllableObject.GetComponent<Animator>();

                // Face the object towards the camera at start
                Vector3 directionToCamera = mainCamera.transform.position - controllableObject.transform.position;
                controllableObject.transform.rotation = Quaternion.LookRotation(new Vector3(directionToCamera.x, 0, directionToCamera.z));
            }
            else
            {
                Debug.LogWarning("Controllable object not found yet. Waiting for spawn.");
            }
        }

        // Handle touch input for setting the target position
        if (Input.touchCount > 0 && controllableObject != null)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = mainCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Began)
            {
                // Check if we hit the collectable object (snowflake)
                if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("CollectableObject"))
                {
                    Debug.Log("Collectable object touched!");
                    // Set the target position to the collectable object's position
                    targetPosition = hit.transform.position;
                    isMoving = true;

                    // Start running animation
                    if (animator != null)
                    {
                        animator.SetBool("IsRunning", true);
                    }

                    // Log the collectable object's position
                    Debug.Log("Moving towards collectable object at: " + targetPosition);
                }
                // Check if we hit a plane
                else if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag(planeTag))
                {
                    // Set target position to the touched point
                    targetPosition = hit.point;
                    isMoving = true;

                    // Face the object toward the target position
                    Vector3 moveDirection = targetPosition - controllableObject.transform.position;
                    if (moveDirection != Vector3.zero)
                    {
                        controllableObject.transform.rotation = Quaternion.LookRotation(moveDirection);
                    }

                    // Start running animation
                    if (animator != null)
                    {
                        animator.SetBool("IsRunning", true);
                    }

                    Debug.Log("Moving to target position: " + targetPosition);
                }
            }
        }

        // Move the character toward the target position if moving
        if (isMoving && controllableObject != null)
        {
            // Move the character incrementally towards the target position
            controllableObject.transform.position = Vector3.MoveTowards(controllableObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if character reached the target position
            if (Vector3.Distance(controllableObject.transform.position, targetPosition) < 0.05f)
            {
                isMoving = false;

                // Stop running animation
                if (animator != null)
                {
                    animator.SetBool("IsRunning", false);
                }

                Debug.Log("Reached target position");
            }
        }
    }

    // Set the collectable object (snowflake) that will be interacted with
    public void SetCollectableObject(GameObject collectableObject)
    {
        this.collectableObject = collectableObject;
        Debug.Log("Collectable object set to: " + collectableObject.name);
    }
}









//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CharacterControl : MonoBehaviour
//{
//    private Camera mainCamera;
//    private GameObject controllableObject;
//    private Animator animator;
//    public string planeTag = "DetectedPlane";
//    private Vector3 targetPosition;
//    private bool isMoving = false;
//    public float moveSpeed = 0.1f;

//    private void Start()
//    {
//        mainCamera = Camera.main;
//    }

//    private void Update()
//    {
//        // Check if the controllable object is assigned
//        if (controllableObject == null)
//        {
//            controllableObject = GameObject.FindGameObjectWithTag("ControllableObject");

//            if (controllableObject != null)
//            {
//                Debug.Log("Controllable object found: " + controllableObject.name);
//                animator = controllableObject.GetComponent<Animator>();

//                // Face the object towards the camera at start
//                Vector3 directionToCamera = mainCamera.transform.position - controllableObject.transform.position;
//                controllableObject.transform.rotation = Quaternion.LookRotation(new Vector3(directionToCamera.x, 0, directionToCamera.z));
//            }
//            else
//            {
//                Debug.LogWarning("Controllable object not found yet. Waiting for spawn.");
//            }
//        }

//        // Handle touch input for setting the target position
//        if (Input.touchCount > 0 && controllableObject != null)
//        {
//            Touch touch = Input.GetTouch(0);
//            Ray ray = mainCamera.ScreenPointToRay(touch.position);
//            RaycastHit hit;

//            if (touch.phase == TouchPhase.Began)
//            {
//                // Check if we hit a plane
//                if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag(planeTag))
//                {
//                    // Set target position to the touched point
//                    targetPosition = hit.point;
//                    isMoving = true;

//                    // Face the object toward the target position
//                    Vector3 moveDirection = targetPosition - controllableObject.transform.position;
//                    if (moveDirection != Vector3.zero)
//                    {
//                        controllableObject.transform.rotation = Quaternion.LookRotation(moveDirection);
//                    }

//                    // Start running animation
//                    if (animator != null)
//                    {
//                        animator.SetBool("IsRunning", true);
//                    }

//                    Debug.Log("Moving to target position: " + targetPosition);
//                }
//            }
//        }

//        // Move the character toward the target position if moving
//        if (isMoving && controllableObject != null)
//        {
//            // Move the character incrementally towards the target position
//            controllableObject.transform.position = Vector3.MoveTowards(controllableObject.transform.position, targetPosition, moveSpeed * Time.deltaTime);

//            // Check if character reached the target position
//            if (Vector3.Distance(controllableObject.transform.position, targetPosition) < 0.05f)
//            {
//                isMoving = false;

//                // Stop running animation
//                if (animator != null)
//                {
//                    animator.SetBool("IsRunning", false);
//                }

//                Debug.Log("Reached target position");
//            }
//        }
//    }
//}
