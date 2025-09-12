using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
