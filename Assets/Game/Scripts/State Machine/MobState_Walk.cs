
using UnityEngine;
using UnityEngine.AI;

public class MobState_Walk : MobStateBase
{
    private float timer;
    private Vector3 randomDirection;
    private Vector3 finalPosition;
    public float chaseDistance = 10f;

    public override void OnEnterState()
    {
        timer = 0;
        Machine.mobAnimator.SetFloat("Speed", 0.5f);
        MoveToRandomPoint();
    }

    public override void OnExitState()
    {
        navMeshAgent.SetDestination(navMeshAgent.gameObject.transform.position);
    }

    public override void OnUpdateState()
    {
        timer += Time.deltaTime;
        base.OnUpdateState();
        if (Vector3.Distance(navMeshAgent.gameObject.transform.position, finalPosition) < 1.5f)
        {
            MoveToRandomPoint();
        }
        if (mainPlayer != null && distanceToPlayer < chaseDistance)
        {
            Machine.SwitchState(Machine.runState);
            return;
        }
        else if (timer > 7)
        {
            Machine.SwitchState(Machine.idleState);
            return;
        }
    }

    private void MoveToRandomPoint()
    {
        randomDirection = Random.insideUnitSphere * 20f;
        randomDirection += navMeshAgent.gameObject.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 20f, 1);
        finalPosition = hit.position;
        navMeshAgent.SetDestination(finalPosition);
    }
}

