using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public float doorSpeed = 2f;
    public float closeDelay;
    private float timeOpen = 0f;

    private Vector3 initialLeftDoorPosition;
    private Vector3 initialRightDoorPosition;
    public Vector3 openLeftPosition;
    public Vector3 openRightPosition;
    private bool isOpen = false;

    void Start()
    {
        initialLeftDoorPosition = leftDoor.position;
        initialRightDoorPosition = rightDoor.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen && other.CompareTag("Player"))
        {
            StartCoroutine(OpenDoorCoroutine());
        }
    }

    IEnumerator OpenDoorCoroutine()
    {
        isOpen = true;

        while (leftDoor.position != openLeftPosition || rightDoor.position != openRightPosition)
        {
            // Move the left door towards the open position
            leftDoor.position = Vector3.MoveTowards(leftDoor.position, openLeftPosition, doorSpeed * Time.deltaTime);
            // Move the right door towards the open position
            rightDoor.position = Vector3.MoveTowards(rightDoor.position, openRightPosition, doorSpeed * Time.deltaTime);
            yield return null;
        }

        // Wait for the specified close delay
        yield return new WaitForSeconds(closeDelay);

        // Close the door after the delay
        while (leftDoor.position != initialLeftDoorPosition || rightDoor.position != initialRightDoorPosition)
        {
            // Move the left door towards the initial position
            leftDoor.position = Vector3.MoveTowards(leftDoor.position, initialLeftDoorPosition, doorSpeed * Time.deltaTime);
            // Move the right door towards the initial position
            rightDoor.position = Vector3.MoveTowards(rightDoor.position, initialRightDoorPosition, doorSpeed * Time.deltaTime);
            yield return null;
        }

        // Reset isOpen flag
        isOpen = false;
    }
}
