using UnityEngine;

public class PlayerState_Roll : PlayerStateBase
{
    public float rollMultiplier = 1f;
    public float duration = 1f;
    public float speedChangeDuration = 0.3f;
    public AnimationCurve rollCurve;
    public AnimationCurve speedChangeCurve;
    private float timer;

    public override void OnEnterState()
    {
        base.OnEnterState();
        timer = 0;
        Machine.Animator.SetBool("isDodging", true);
        Machine.Animator.SetInteger("DodgeNum", 2);
        Machine.lastDirection = Machine.Controller.gameObject.transform.forward;
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        timer += Time.deltaTime;
        if (timer > duration)
        {
            Machine.SwitchState(Machine.idleState);
        }
        if (timer < speedChangeDuration)
        {
            Machine.currentAcceleration = Mathf.MoveTowards(Machine.currentAcceleration, speedChangeCurve.Evaluate(timer / speedChangeDuration) * Machine.currentAcceleration, Time.deltaTime * Machine.acceleration);
        }
        else
        {
            Machine.currentAcceleration = Mathf.MoveTowards(Machine.currentAcceleration, rollCurve.Evaluate(timer) * rollMultiplier, Time.deltaTime * Machine.acceleration);
        }
        Machine.Controller.Move(Machine.lastDirection.normalized * Machine.currentAcceleration * Time.deltaTime);
    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("isDodging", false);
    }
}
