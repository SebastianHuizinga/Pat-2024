using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelBullets : MonoBehaviour
{
    public float bulletDamageIncrease = 10f; // Amount to increase bullet damage

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting playerShooting = other.GetComponent<PlayerShooting>();
            if (playerShooting != null)
            {
                // Access bullet prefab from PlayerShooting and increase its base damage
                Bullet bulletPrefab = playerShooting.bulletPrefab.GetComponent<Bullet>();
                if (bulletPrefab != null)
                {
                    bulletPrefab.baseDamage += bulletDamageIncrease;
                    Debug.Log("Steel bullets applied! Damage increased by " + bulletDamageIncrease);

                    // Add item to player's save data
                    UserSaveData saveData = other.GetComponent<UserSaveData>();
                    if (saveData != null)
                    {
                        saveData.AddItem("SteelBullets");  // Add the steel bullets item to player's inventory
                        Debug.Log("SteelBullets added to inventory.");
                    }
                }
            }

            Destroy(gameObject);  // Destroy the item after pickup
        }
    }
}
