using UnityEngine;

public class PlateInteraction : MonoBehaviour
{
    public BakingManager bakingManager; // Reference to BakingManager

    void OnMouseDown()
    {
        // When the plate is clicked, call the StartBaking method in BakingManager
        if (bakingManager != null)
        {
            bakingManager.StartBaking();
        }
        else
        {
            Debug.LogError("BakingManager reference is missing!");
        }
    }
}
