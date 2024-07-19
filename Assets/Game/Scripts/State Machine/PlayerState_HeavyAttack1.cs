using UnityEngine;

public class PlayerState_HeavyAttack1 : BasePlayerAttackState
{
    public MobStateMachine mobMachine;

    public override void OnEnterState()
    {
        base.OnEnterState();
        Machine.Animator.SetBool("isHeavyAttack1", true);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        Machine.Controller.Move(Vector3.zero);

    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("isHeavyAttack1", false);
        Machine.heavyAttack2.mobMachine = mobMachine;
        Machine.heavyAttack3.mobMachine = mobMachine;
    }
}
