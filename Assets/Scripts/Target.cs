// Purpose: This script is used to manage the health of the target object. It is attached to the target object in the scene.

using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 50f; // Initial health
    private Transform initialScale;

    Transform deadTransform;

  //  Animator anim;

    void Start()
    {
        initialScale = transform;
    }

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
//        anim.SetInteger("animState", 3);

        Debug.Log(gameObject.name + " died.");
        
        deadTransform = gameObject.transform;

        // Destroy(gameObject, 4);
        StartCoroutine("ScaleObject");
    }
    
    IEnumerator ScaleObject()
    {
        float timeElapsed = 0f;
        while (timeElapsed < 3f)
        {
            float t = timeElapsed / 3f;
            transform.localScale = Vector3.Lerp(initialScale.localScale, Vector3.zero, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject , 0.2f);
    }
}