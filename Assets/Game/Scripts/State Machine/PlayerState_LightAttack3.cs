using UnityEngine;

public class PlayerState_LightAttack3 : BasePlayerAttackState
{
    public override void OnEnterState()
    {
        base.OnEnterState();
        if (!Machine.LightAttack)
        {
            Machine.Animator.SetTrigger("isLightAttack");
        }
        Machine.Animator.SetInteger("LightAttackNum", 2);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        Machine.Controller.Move(Vector3.zero);

    }

    public override void OnExitState()
    {
        Machine.Animator.SetTrigger("isLightAttack");
    }
}
