// Purpose: This script is used to manage the health of the target object. It is attached to the target object in the scene.

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Target : MonoBehaviour
{

    public float health = 50f; // Initial health
    private bool alive = true;
    [SerializeField] private Slider healthBar;
    private float initalHealth;
    private bool healthBarDestoryed = false;

    private void Start()
    {
        initalHealth = health;
        healthBar.maxValue = initalHealth;
        healthBar.value = health;
    }

    private void Update()
    {
        if (health <= 0)
        {
            alive = false;
        }

        health = Mathf.Clamp(health, 0, initalHealth);
        if (!healthBarDestoryed)
        {
            healthBar.value = health;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            alive = false;
            if (!healthBarDestoryed)
            {
                Destroy(healthBar.gameObject);
                healthBarDestoryed = true;
            }
        }
    }

    public bool IsAlive()
    {
        return alive;
    }

//     void Die()
//     {
// //        anim.SetInteger("animState", 3);
//
//         Debug.Log(gameObject.name + " died.");
//         
//         deadTransform = gameObject.transform;
//
//         // Destroy(gameObject, 4);
//         StartCoroutine("ScaleObject");
//     }
//     
//     IEnumerator ScaleObject()
//     {
//         float timeElapsed = 0f;
//         while (timeElapsed < 3f)
//         {
//             float t = timeElapsed / 3f;
//             transform.localScale = Vector3.Lerp(initialScale.localScale, Vector3.zero, t);
//             timeElapsed += Time.deltaTime;
//             yield return null;
//         }
//         Destroy(gameObject , 0.2f);
//     }
}