using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobState_Attack : MobStateBase
{
    private PlayerHealth playerHealth;
    private float timer;
    [SerializeField] private float damageDelay;
    [SerializeField] private float duration;
    private bool dealtDamage;
    public override void OnEnterState()
    {
        Machine.mobAnimator.SetBool("isAttacking", true);
        playerHealth = mainPlayer.GetComponentInParent<PlayerHealth>();
        timer = 0;
        dealtDamage = false;
        playerHealth.mobMachine = Machine;
    }

    public override void OnExitState()
    {
        Machine.mobAnimator.SetBool("isAttacking", false);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        timer += Time.deltaTime;
        
        if (distanceToPlayer > 1.5f)
        {
            Machine.SwitchState(Machine.runState);
            return;
        }
        else if (timer >= damageDelay && !dealtDamage)
        {
            playerHealth.TakeDamage(30f);
            dealtDamage = true;
        }

        if (timer >= duration)
        {
            Machine.SwitchState(Machine.idleState);

            return;
        }

    }
}

