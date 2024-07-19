using UnityEngine;

public class PlayerState_Parry : PlayerStateBase
{
    private float timer;
    public float duration = 1f;

    public override void OnEnterState()
    {
        base.OnEnterState();
        timer = 0;
        Machine.Animator.SetBool("isDodging", true);
        Machine.Animator.SetInteger("DodgeNum", 3);
        Machine.PlayerHealth.isParried = true;
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        timer += Time.deltaTime;
        if (timer > duration)
        {
            Machine.SwitchState(Machine.idleState);
            return;
        }
        Machine.Controller.Move(Vector3.zero);
    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("isDodging", false);
        Machine.PlayerHealth.isParried = false;
        Machine.Parry = false;
    }
}
