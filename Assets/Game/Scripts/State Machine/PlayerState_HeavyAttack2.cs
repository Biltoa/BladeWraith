using UnityEngine;

public class PlayerState_HeavyAttack2 : BasePlayerAttackState
{
    public MobStateMachine mobMachine;

    public override void OnEnterState()
    {
        base.OnEnterState();
        Machine.Animator.SetBool("isHeavyAttack2", true);
    }

    public override void OnUpdateState()
    {
        if (mobMachine != null)
        {
            mobMachine.MobHealth.isKnockedUp = true;
        }
        base.OnUpdateState();
        Machine.Controller.Move(Vector3.zero);
    }

    public override void OnExitState()
    {
        if (mobMachine != null)
        {
            mobMachine.MobHealth.isKnockedUp = false;
        }
        Machine.Animator.SetBool("isHeavyAttack2", false);
    }
}
