using UnityEngine;

public class MobStateMachine : StateMachineBase
{
    public MobState_Idle idleState;
    public MobState_Run runState;
    public MobState_Walk walkState;
    public MobState_Attack attackState;
    public MobState_TakeDamage takeDamage;
    public MobState_Death deathState;
    public MobState_KnockupDamage knockupDamage;
    public Animator mobAnimator;
    public MobHealth MobHealth;
    protected override void SetInitialState()
    {
        currentState = idleState;
    }
}

