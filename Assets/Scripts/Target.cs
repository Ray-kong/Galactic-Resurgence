// Purpose: This script is used to manage the health of the target object. It is attached to the target object in the scene.
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f; // Initial health

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died.");
        Destroy(gameObject); 
    }
}