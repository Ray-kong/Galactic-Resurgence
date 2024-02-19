using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpHeight = 3f;
    public float gravity = 9.8f;
    
    [Header("Health Settings")]
    public float maxHealth = 100.0f;
    public float currentHealth = 50.0f;
    
    [Header("Attachable")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private GameObject  gun;
    
    private Vector3 move, direction; 
    private CharacterController controller;
    
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        Vector3 inputDirection = GetInputDirection();
        Vector3 moveDirection = CalculateMovement(inputDirection);

        if (controller.isGrounded)
        {
            direction = new Vector3(moveDirection.x, direction.y, moveDirection.z);
        }

        direction.y = CalculateJump(direction.y);
        direction.y = ApplyGravity(direction.y);

        MoveCharacter(moveDirection, direction.y);
    }

    #region Input Handling
    private Vector3 GetInputDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        return new Vector3(x, 0f, z);
    }
    #endregion

    #region Movement Calculation
    private Vector3 CalculateMovement(Vector3 inputDirection)
    {
        // Use the camera's right and forward vectors for movement direction
        Vector3 moveRight = mainCamera.right * inputDirection.x;
        Vector3 moveForward = Vector3.ProjectOnPlane(mainCamera.forward, Vector3.up).normalized * inputDirection.z;
        return (moveRight + moveForward).normalized;
    }
    #endregion

    #region Jump Handling
    private float CalculateJump(float currentYVelocity)
    {
        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            return Mathf.Sqrt(2f * gravity * jumpHeight);
        }
        return currentYVelocity;
    }
    #endregion

    #region Apply Gravity
    private float ApplyGravity(float currentYVelocity)
    {
        if (!controller.isGrounded)
        {
            currentYVelocity -= gravity * Time.deltaTime;
        }
        return currentYVelocity;
    }
    #endregion

    #region Character Movement
    private void MoveCharacter(Vector3 moveDirection, float yVelocity)
    {
        Vector3 finalMove = new Vector3(moveDirection.x, yVelocity, moveDirection.z) * (Time.deltaTime * speed);
        controller.Move(finalMove);
    }
    #endregion

    # region Health and PowerUp Handling
    public IEnumerator AdjustSpeed(float multiplier, float duration)
    {
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed /= multiplier;
    }

    // Coroutine for adjusting jump force
    public IEnumerator AdjustJumpForce(float multiplier, float duration)
    {
        jumpHeight *= multiplier;
        yield return new WaitForSeconds(duration);
        jumpHeight /= multiplier;
    }

    
    public IEnumerator AdjustDamage(float multiplier, float duration)
    {
        
        gun.GetComponent<Gun>().damage *= multiplier;
        yield return new WaitForSeconds(duration);
        gun.GetComponent<Gun>().damage /= multiplier;
    }
    
    public void ModifyHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevents going over max or below 0
        // Update health UI
        if (currentHealth <= 0)
        {
            // Die
            Destroy(gameObject);
        }
    }
    #endregion
}