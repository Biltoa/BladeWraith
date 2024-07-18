using UnityEngine;

public class PlayerState_HeavyAttack2 : BasePlayerAttackState
{
    public MobStateMachine mobMachine;

    public override void OnEnterState()
    {
        base.OnEnterState();
        if (!Machine.HeavyAttack)
        {
            Machine.Animator.SetTrigger("isHeavyAttack");
        }
        Machine.Animator.SetInteger("HeavyAttackNum", 1);
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
        if (!Machine.HeavyAttack)
        {
            Machine.Animator.SetTrigger("isHeavyAttack");
        }
    }
}
