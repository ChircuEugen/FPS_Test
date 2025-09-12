using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    private Animator animator;
    private EnemyStateManager manager;
    private NavMeshAgent agent;

    private int health;
    public int maxHealth;

    bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        manager = GetComponent<EnemyStateManager>();
        agent = GetComponent<NavMeshAgent>();

        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

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
        manager.enabled = false;
        agent.enabled = false;
    }
}
