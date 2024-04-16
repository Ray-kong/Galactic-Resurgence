using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TwoDLevelManager.isGameOver == false)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  Debug.Log("w");
        if(collision.gameObject.tag == "Boundary")
        {

            transform.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
            moveSpeed *= -1;
        }
    }
}
