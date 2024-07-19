using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobState_TakeDamage : MobStateBase
{
    private float timer;
    public float FallTime;
    public bool isFalling;
    public AudioSource MobDeath;
    public AudioSource MobHit;
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    private bool isDead;
    public override void OnEnterState()
    {
        base.OnEnterState();
        StartCoroutine(ChangeColor());
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
            return;
        }
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        timer += Time.deltaTime;
        if (timer >= FallTime)
        {
            Machine.SwitchState(Machine.idleState);
            return;
        }
    }

    public override void OnExitState()
    {
    }

    private IEnumerator ChangeColor()
    {
        for (int i = 0; i < SkinnedMeshRenderer.materials.Length; i++)
        {
            SkinnedMeshRenderer.materials[i].color = Color.red;
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < SkinnedMeshRenderer.materials.Length; i++)
        {
            SkinnedMeshRenderer.materials[i].color = Color.white;
        }
    }
}
