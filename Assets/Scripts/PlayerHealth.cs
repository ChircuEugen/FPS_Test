using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int health;
    public ParticleSystem bloodEffect;
    private Vector3 bloodOffset = new Vector3(0, 1.3f, 0);

    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;
    public Transform cameraLookPoint;
    public Rig rig;
    public HealthBar healthBar;

    bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        health -= damage;
        Instantiate(bloodEffect, hitPoint + bloodOffset, Quaternion.identity);
        healthBar.SetHealth(health);

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Died");
        playerMovement.enabled = false;
        playerShooter.enabled = false;
        rig.weight = 0f;
        animator.SetLayerWeight(1, 0f);
        cameraLookPoint.transform.localPosition = new Vector3(-0.2f, 0.55f, -1f);
    }
}
