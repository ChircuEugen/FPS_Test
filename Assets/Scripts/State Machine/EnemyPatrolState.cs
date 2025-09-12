using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    Vector3 destination = Vector3.zero;

    public override void EnterState(EnemyStateManager manager)
    {
        manager.agent.isStopped = false;

        if (destination == Vector3.zero)
        {
            destination = manager.patrolPointA;
        }

        manager.animator.SetBool("IsMoving", true);
        manager.animator.SetFloat("MoveType", 0);
    }

    public override void UpdateState(EnemyStateManager manager)
    {
        manager.agent.SetDestination(destination);

        if (!manager.agent.pathPending && manager.agent.remainingDistance <= manager.agent.stoppingDistance)
        {
            if (destination == manager.patrolPointA)
            {
                destination = manager.patrolPointB;
            }
            else
            {
                destination = manager.patrolPointA;
            }
        }

        if (Vector3.Distance(manager.transform.position, manager.player.transform.position) < manager.chaseTriggerDistance)
        {
            manager.SwitchState(manager.chaseState);
        }
    }
}
