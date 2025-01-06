using UnityEngine;

public class IngredientToggle : MonoBehaviour
{
    public GameObject[] ingredientParticles; // Array of particle prefabs for ingredients

    // Toggles the particle prefab based on the button index
    public void ToggleIngredient(int index)
    {
        if (index >= 0 && index < ingredientParticles.Length && ingredientParticles[index] != null)
        {
            // Toggle the active state
            bool isActive = ingredientParticles[index].activeSelf;
            ingredientParticles[index].SetActive(!isActive);
            Debug.Log($"Ingredient {index} toggled to {(!isActive ? "active" : "inactive")}");
        }
        else
        {
            Debug.LogError($"Invalid index or particle prefab not assigned for index {index}");
        }
    }
}
