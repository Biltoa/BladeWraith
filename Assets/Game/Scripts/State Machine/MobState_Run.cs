using UnityEngine;
using UnityEngine.AI;

public class MobState_Run : MobStateBase
{
    public float stoppingDistance = 1f;
    public float chaseDistance = 10f;
    public PlayerHealth playerHealth;

    public override void OnEnterState()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        navMeshAgent.stoppingDistance = stoppingDistance;
        navMeshAgent.speed = 3f;
        Machine.mobAnimator.SetFloat("Speed", 1);

    }

    public override void OnExitState()
    {
        navMeshAgent.SetDestination(navMeshAgent.gameObject.transform.position);
        navMeshAgent.speed = 1f;

    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        if (mainPlayer != null && distanceToPlayer < chaseDistance)
        {
            navMeshAgent.SetDestination(mainPlayer.position);
        }
        if (distanceToPlayer <= 1.5f && playerHealth.health > 0)
        {
            Machine.SwitchState(Machine.attackState);
        }
        if (distanceToPlayer > 10f)
        {
            Machine.SwitchState(Machine.walkState);
        }
    }
}

