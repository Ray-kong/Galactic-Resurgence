using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{

    public Transform player;

    // Update is called once per frame
    void Update()
    {
        // Delete "new Vector3(0, 2, -10)" when we are sure everything works
        transform.position = player.transform.position + new Vector3(0, 2, -10);
    }
}
