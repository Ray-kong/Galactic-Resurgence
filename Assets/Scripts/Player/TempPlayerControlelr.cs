using UnityEngine;

public class TempPlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 3f;
    public float gravity = 9.8f;
    public Transform cameraTransform; // Reference to the Camera Transform
    
    private Vector3 _move, _direction; 
    private CharacterController _controller;
    
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Use the camera's right and forward vectors for movement direction
        Vector3 moveRight = cameraTransform.right * x;
        Vector3 moveForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized * z;

        _move = (moveRight + moveForward).normalized;
        
        if(_controller.isGrounded) {
            _direction = new Vector3(_move.x, _direction.y, _move.z);
            if(Input.GetButton("Jump")) {
                _direction.y = Mathf.Sqrt(2f * gravity * jumpHeight);
            }
        }
        
        _direction.y -= gravity * Time.deltaTime; // Apply gravity
        _controller.Move(_direction * Time.deltaTime * speed); // Move the character controller
    }
}