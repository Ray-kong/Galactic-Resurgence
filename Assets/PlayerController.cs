using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // public speed and jump variables
    public float speed = 50f;
    public float jumpForce = 5f;
    public float maxHealth = 100.0f;
    public float currentHealth = 50.0f;
    public float gunDamage = 10.0f;

    private Rigidbody rb;
    private bool isGrounded = true;




    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(x, 0.0f, z);

        rb.AddForce(movement);

        // Jump mechanic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        Debug.Log("Speed: " + speed);
        Debug.Log("Jump: " + jumpForce);
        Debug.Log("Health: " + currentHealth);
    }

    // Check if the player is grounded
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    // Coroutine for adjusting speed
    public IEnumerator AdjustSpeed(float multiplier, float duration)
    {
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed /= multiplier;
    }

    // Coroutine for adjusting jump force
    public IEnumerator AdjustJumpForce(float multiplier, float duration)
    {
        jumpForce *= multiplier;
        yield return new WaitForSeconds(duration);
        jumpForce /= multiplier;
    }

    public IEnumerator AdjustDamage(float multiplier, float duration)
    {
        gunDamage *= multiplier;
        yield return new WaitForSeconds(duration);
        gunDamage /= multiplier;
    }



    public void ModifyHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevents going over max or below 0
        // Update health UI
        if (currentHealth <= 0)
        {
            // Handle player death
        }
    }
}