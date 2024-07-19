using UnityEngine;

public class PlayerState_LightAttack2 : BasePlayerAttackState
{
    public override void OnEnterState()
    {
        base.OnEnterState();
        Machine.Animator.SetBool("isLightAttack2", true);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        Machine.Controller.Move(Vector3.zero);
    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("isLightAttack2", false);
    }
}
