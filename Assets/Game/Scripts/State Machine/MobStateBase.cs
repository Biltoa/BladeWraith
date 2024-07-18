using UnityEngine;
using UnityEngine.AI;

public abstract class MobStateBase : StateBase
{
    public MobStateMachine Machine;
    public Transform mainPlayer;
    protected float distanceToPlayer;
    public NavMeshAgent navMeshAgent;

    protected void OnEnable()
    {
        mainPlayer = FindFirstObjectByType<CharacterController>().transform;
    }
    public override void OnEnterState()
    {
    }

    public override void OnUpdateState() 
    {
        distanceToPlayer = Vector3.Distance(mainPlayer.position, transform.position);
    }

    public override void OnExitState() { }

    public override bool CanEnterState() { return true; }
}