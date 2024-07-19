using System.Collections;
using UnityEngine;

public class PlayerState_TakeDamage : PlayerStateBase
{
    private float timer;
    public AudioSource PlayerDeath;
    public AudioSource PlayerHit;
    public float Duration = 0.5f;
    public override void OnEnterState()
    {
        base.OnEnterState();
        timer = 0;
        Machine.isLightAttack = false;
        Machine.isHeavyAttack = false;
        Machine.Animator.SetBool("Reset", true);
        if (Machine.PlayerHealth.health > 0)
        {
            Machine.Animator.SetTrigger("isHit");
            PlayerHit.Play();
        }
        else
        {
            Machine.Animator.SetTrigger("isDead");
            Machine.UIManager.GameLose();
            PlayerDeath.Play();
            StartCoroutine(DeathDelay());
        }
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        Machine.Controller.Move(Vector3.zero);
        timer += Time.deltaTime;
        if (timer >= Duration)
        {
            Machine.SwitchState(Machine.idleState);
            return;
        }
    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("Reset", false);
        Machine.idleState.IsTakingDamage = false;
        Machine.walkState.IsTakingDamage = false;
    }
    
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }
}
