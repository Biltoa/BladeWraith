using UnityEngine;

public class PlayerState_HeavyAttack1 : BasePlayerAttackState
{
    public MobStateMachine mobMachine;

    public override void OnEnterState()
    {
        base.OnEnterState();
        Machine.Animator.SetTrigger("isHeavyAttack");
        Machine.Animator.SetInteger("HeavyAttackNum", 0);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        Machine.Controller.Move(Vector3.zero);

    }

    public override void OnExitState()
    {
        if (!Machine.HeavyAttack)
        {
            Machine.Animator.SetTrigger("isHeavyAttack");
        }
        Machine.heavyAttack2.mobMachine = mobMachine;
        Machine.heavyAttack3.mobMachine = mobMachine;
    }
}
