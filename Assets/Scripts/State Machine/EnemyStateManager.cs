using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;

    public EnemyPatrolState patrolState = new EnemyPatrolState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyAttackState attackState = new EnemyAttackState();

    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Transform player;

    public Vector3 patrolPointA;
    public Vector3 patrolPointB;
    public float chaseTriggerDistance = 3f;
    public float forgetDistance = 7f;
    public float attackDistance = 1.3f;
    public float attackRate = 1.3f;
    public int enemyDamage = 25;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        currentState = patrolState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
