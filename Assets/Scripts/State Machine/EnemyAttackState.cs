using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    float timer;
    public override void EnterState(EnemyStateManager manager)
    {
        manager.animator.SetBool("IsMoving", false);
        manager.animator.SetTrigger("Attacked");
        manager.agent.isStopped = true;
        timer = 99f;
    }

    public override void UpdateState(EnemyStateManager manager)
    {
        timer += Time.deltaTime;
        if (timer >= manager.attackRate)
        {
            timer = 0f;
            IDamageable playerHealth = manager.player.GetComponent<IDamageable>();
            playerHealth.TakeDamage(manager.enemyDamage, manager.player.position);
        }

        if (Vector3.Distance(manager.transform.position, manager.player.position) > manager.attackDistance)
        {
            manager.SwitchState(manager.chaseState);
        }

    }

}
