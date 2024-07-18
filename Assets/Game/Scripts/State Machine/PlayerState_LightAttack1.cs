using UnityEngine;

public class PlayerState_LightAttack1 : BasePlayerAttackState
{
    public override void OnEnterState()
    {
        base.OnEnterState();
        Machine.Animator.SetTrigger("isLightAttack");
        Machine.Animator.SetInteger("LightAttackNum", 0);
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
