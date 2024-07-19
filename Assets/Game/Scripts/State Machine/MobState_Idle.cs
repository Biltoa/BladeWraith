using UnityEngine;
using UnityEngine.AI;

public class MobState_Idle : MobStateBase
{
    private float timer;
    public float chaseDistance = 10f;

    public override void OnEnterState()
    {
        timer = 0;
        Machine.mobAnimator.SetFloat("Speed", 0);
        Machine.mobAnimator.SetBool("isHitFalling", false);
        Machine.takeDamage.isFalling = false;


        if (Machine.GetComponent<NavMeshAgent>().enabled == false)
        {
            Machine.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    public override void OnExitState()
    {
    }

    public override void OnUpdateState()
    {
        timer += Time.deltaTime;
        base.OnUpdateState();
        if (mainPlayer != null && distanceToPlayer < chaseDistance)
        {
            Machine.SwitchState(Machine.runState);
            return;
        }
        else if (timer > 3)
        {
            Machine.SwitchState(Machine.walkState);
            return;
        }
    }
}

