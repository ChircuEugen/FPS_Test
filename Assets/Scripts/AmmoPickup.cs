using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoToIncrease = 30;
    public AudioClip pickUpSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooter playerShooter = other.GetComponent<PlayerShooter>();
            playerShooter.IncreaseAmmo(ammoToIncrease);
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
            Destroy(gameObject);
        }
    }
}
