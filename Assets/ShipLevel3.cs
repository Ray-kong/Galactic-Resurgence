using UnityEngine;

public class ShipLevel3 : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.LoadLevel("ExplosionScene");
        }
    }
}
