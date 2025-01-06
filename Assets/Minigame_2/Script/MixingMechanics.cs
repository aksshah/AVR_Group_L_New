using UnityEngine;

public class MixingMechanics : MonoBehaviour
{
    [SerializeField] private GameObject spoonPrefab;
    [SerializeField] private GameObject bowlPrefab;
    private GameObject spoonInstance;
    private GameObject bowlInstance;

    public float rotationSpeed = 50f;

    void Start()
    {
        // Instantiate the spoon and bowl at default positions
        bowlInstance = Instantiate(bowlPrefab, Vector3.zero, Quaternion.identity);
        spoonInstance = Instantiate(spoonPrefab, new Vector3(0.3f, 0, 0), Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateSpoons(-rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateSpoons(rotationSpeed);
        }
    }

    public void RotateSpoons(float rotationSpeed)
    {
        if (spoonInstance != null)
        {
            spoonInstance.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Spoon instance not found!");
        }
    }
}
