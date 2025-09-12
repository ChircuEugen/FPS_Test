using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager manager)
    {
        manager.agent.isStopped = false;

        manager.animator.SetBool("IsMoving", true);
        manager.animator.SetFloat("MoveType", 1);
    }

    public override void UpdateState(EnemyStateManager manager)
    {
        manager.agent.SetDestination(manager.player.position);

        if (Vector3.Distance(manager.transform.position, manager.player.transform.position) > manager.forgetDistance)
        {
            manager.SwitchState(manager.patrolState);
        }
        else if (Vector3.Distance(manager.transform.position, manager.player.transform.position) < manager.attackDistance)
        {
            manager.SwitchState(manager.attackState);
        }
    }
}
