using UnityEngine;

public class PlayerState_Block : PlayerStateBase
{
    public bool IsAttacked;
    public override void OnEnterState()
    {
        base.OnEnterState();
        Machine.Animator.SetBool("isDodging", true);
        Machine.Animator.SetInteger("DodgeNum", 0);
        Machine.PlayerHealth.isBlocked = true;
        IsAttacked = false;
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        if (IsAttacked)
        {
            Machine.Animator.SetTrigger("AttackBlocked");
            IsAttacked = false;
        }
        if (!Machine.Block)
        {
            Machine.SwitchState(Machine.idleState);
        }
        Machine.Controller.Move(Vector3.zero);
    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("isDodging", false);
        Machine.PlayerHealth.isBlocked = false;
    }
}
