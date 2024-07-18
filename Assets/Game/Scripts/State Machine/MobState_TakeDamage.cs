using UnityEngine;
using UnityEngine.AI;

public class MobState_TakeDamage : MobStateBase
{
    private float timer;
    public float FallTime;
    public bool isFalling;
    public AudioSource MobDeath;
    public AudioSource MobHit;
    private bool isDead;
    public override void OnEnterState()
    {
        base.OnEnterState();
        timer = 0;
        if (Machine.MobHealth.health > 0 && !isFalling)
        {
            Machine.mobAnimator.SetTrigger("isHit");
            MobHit.Play();
        }
        else if (Machine.MobHealth.health > 0 && isFalling)
        {
            Machine.mobAnimator.SetBool("isHitFalling", true);
            MobHit.Play();
        }
        else
        {
            Machine.SwitchState(Machine.deathState);
            if (!isDead)
            {
                MobDeath.Play();
                isDead = true;
                int coins = PlayerPrefs.GetInt("Coins") + 100;
                PlayerPrefs.SetInt("Coins", coins);
            }
        }
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        timer += Time.deltaTime;
        if (timer >= FallTime)
        {
            Machine.SwitchState(Machine.idleState);
        }
    }

    public override void OnExitState()
    {
    }
}
