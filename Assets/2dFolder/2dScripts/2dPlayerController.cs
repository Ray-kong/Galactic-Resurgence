using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDPlayerController : MonoBehaviour
{
    public float hiinput;
    public float moveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hiinput = Input.GetAxisRaw("Horizontal") * -1;

        transform.Translate(Vector2.right * hiinput * moveSpeed * Time.deltaTime);
    }
}
