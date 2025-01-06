using UnityEngine;

public class CardamomVisibility : MonoBehaviour
{
    public GameObject cardamomPrefab; // Prefab for the cardamom particle
    public Transform bowl; // Reference to the bowl

    void OnParticleCollision(GameObject other)
    {
        // Get the collision point (where the particles hit)
        Vector3 collisionPoint = other.transform.position;

        // Instantiate the cardamom prefab at the collision point
        GameObject cardamom = Instantiate(cardamomPrefab, collisionPoint, Quaternion.identity);

        // Set the parent of the instantiated cardamom to the bowl
        cardamom.transform.SetParent(bowl); // This modifies only the instantiated object
    }
}
