using UnityEngine;

public class PlayerState_TakeDamage : PlayerStateBase
{
    private float timer;
    public AudioSource PlayerDeath;
    public AudioSource PlayerHit;
    public override void OnEnterState()
    {
        base.OnEnterState();
        timer = 0;
        Machine.isLightAttack = false;
        Machine.isHeavyAttack = false;
        Machine.Animator.SetBool("Reset", true);
        if (Machine.PlayerHealth.health > 0)
        {
            Machine.Animator.SetBool("isHit", true);
            PlayerHit.Play();
        }
        else
        {
            Machine.Animator.SetTrigger("isDead");
            Machine.UIManager.GameLose();
            PlayerDeath.Play();
        }
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        Machine.Controller.Move(Vector3.zero);
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            Machine.SwitchState(Machine.idleState);
        }
    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("isHit", false);
        Machine.Animator.SetBool("Reset", false);
    }
}
