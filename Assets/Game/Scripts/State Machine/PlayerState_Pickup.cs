using UnityEngine;

public class PlayerState_Pickup : PlayerStateBase
{
    private float timer;
    public float Duration;

    public override void OnEnterState()
    {
        timer = 0;
        Machine.Animator.SetBool("isPicking", true);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        timer += Time.deltaTime;
        if (timer > Duration)
        {
            Machine.SwitchState(Machine.idleState);
        }
    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("isPicking", false);
    }
}
