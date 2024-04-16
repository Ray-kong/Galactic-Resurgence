using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public float pickupRange = 3.0f;
    public KeyCode pickupKey = KeyCode.E;
    public GameObject interactText;
    public GameObject Enemies1;
    public GameObject Enemies2;
   
    public AudioSource item;
    public AudioSource uhoh;

    private GameObject player;
    private bool isPlayerNear = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= pickupRange && PlayerInventory.HasKeyLevel1 == false)
        {
            isPlayerNear = true;
            interactText.SetActive(true);
        }
        else
        {
            isPlayerNear = false;
            interactText.SetActive(false);
        }

        if (isPlayerNear && Input.GetKeyDown(pickupKey))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        PlayerInventory.HasKeyLevel1 = true; 
        Enemies1.SetActive(true);
        Enemies2.SetActive(false);
        item.Play();

        uhoh.Play();
    }
}