using UnityEngine;

public class MobState_Death : MobStateBase
{
    private float timer;
    public bool lastDeath;
    public override void OnEnterState()
    {
        Machine.mobAnimator.SetTrigger("isDead");
        timer = 0;
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }
    public override void OnUpdateState()
    {
        base.OnUpdateState();
        timer += Time.deltaTime;
        if (timer > 2f)
        {
            mainPlayer.GetComponentInParent<PlayerStateMachine>().SpawnManager.DespawnMob();
            Destroy(Machine.gameObject);
        }
    }
}

