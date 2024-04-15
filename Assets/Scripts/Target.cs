// Purpose: This script is used to manage the health of the target object. It is attached to the target object in the scene.

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{

    public float health = 50f; // Initial health
    private bool alive = true;
    [SerializeField] private Slider healthBar;
    
    

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            alive = false;
        }
        healthBar.value = health;
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