using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobState_KnockupDamage : MobStateBase
{
    private float timer;
    public AnimationCurve speedCurve;
    public float knockupForce;
    public float knockupSpeed;
    private float currentKnockupForce;
    public AudioSource MobDeath;
    public AudioSource MobHit;
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    private bool isDead;
    public override void OnEnterState()
    {
        base.OnEnterState();
        StartCoroutine(ChangeColor());
        timer = 0;
        currentKnockupForce = knockupForce;
        Machine.GetComponent<NavMeshAgent>().enabled = false;
        if (Machine.MobHealth.health > 0)
        {
            Machine.mobAnimator.SetTrigger("isKnockedUp");
            MobHit.Play();
        }
        else
        {
            Machine.SwitchState(Machine.deathState);

            if (!isDead)
            {
                MobDeath.Play();
                isDead = true;
            }
            return;
        }
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        Machine.mobAnimator.SetFloat("KnockupSpeed", speedCurve.Evaluate(timer)/2);
        currentKnockupForce -= Time.deltaTime * knockupSpeed;
        Vector3 motion = Vector3.up * currentKnockupForce * Time.deltaTime;
        Machine.transform.position += motion;
        if (currentKnockupForce < -knockupForce)
        {
            Machine.SwitchState(Machine.idleState);
            return;
        }
        timer += Time.deltaTime;
        /*if (timer >= 2f)
        {
            Machine.SwitchState(Machine.idleState);
        }*/
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