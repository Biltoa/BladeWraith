using UnityEngine;

public class PlayerState_LightAttack2 : BasePlayerAttackState
{
    public override void OnEnterState()
    {
        base.OnEnterState();
        if (!Machine.LightAttack)
        {
            Machine.Animator.SetTrigger("isLightAttack");
        }
        Machine.Animator.SetInteger("LightAttackNum", 1);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        Machine.Controller.Move(Vector3.zero);

    }

    public override void OnExitState()
    {
        if (!Machine.LightAttack)
        {
            Machine.Animator.SetTrigger("isLightAttack");
        }
    }
}
