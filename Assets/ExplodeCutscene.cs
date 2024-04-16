using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCutscene : MonoBehaviour
{
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explosion", 1);
        Invoke("Explosion2", 2);
        Invoke("Explosion3", 3);
        Invoke("Explosion4", 2);
        Invoke("Explosion5", 2);
        Invoke("Explosion6", 1);
        Invoke("Explosion7", 1);
        Invoke("Explosion", 3);
        Destroy(gameObject, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explosion()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    public void Explosion2()
    {
        Vector3 bruh = new Vector3(-1, 2, 0);
        Instantiate(explosionPrefab, bruh, Quaternion.identity);
    }

    public void Explosion3()
    {
        Vector3 bruh = new Vector3(1, 2, 0);
        Instantiate(explosionPrefab, bruh, Quaternion.identity);
    }

    public void Explosion4()
    {
        Vector3 bruh = new Vector3(-1, -2, 0);
        Instantiate(explosionPrefab, bruh, Quaternion.identity);
    }

    public void Explosion5()
    {
        Vector3 bruh = new Vector3(1, -2, 0);
        Instantiate(explosionPrefab, bruh, Quaternion.identity);
    }

    public void Explosion6()
    {
        Vector3 bruh = new Vector3(-2, -1, 0);
        Instantiate(explosionPrefab, bruh, Quaternion.identity);
    }

    public void Explosion7()
    {
        Vector3 bruh = new Vector3(2, -1, 0);
        Instantiate(explosionPrefab, bruh, Quaternion.identity);
    }
}
