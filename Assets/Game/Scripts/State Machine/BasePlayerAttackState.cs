using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerAttackState : PlayerStateBase
{
    public float duration = 0.5f;
    public BasePlayerAttackState nextAttack;
    public AnimationCurve movementCurve;
    public float movementSpeed;
    public float attackAngle = 40f;
    public float attackRadius = 10f;
    public float damageDelay;
    public float DamageValue;
    public ParticleSystem Particles;
    public float ParticleDelay;
    public AudioSource AudioClip;
    public float AudioTimer;
    protected float timer;
    private bool isDamageDealt;
    protected bool isParticlePlayed;
    protected bool isAudioPlayed;

    public override void OnEnterState()
    {
        base.OnEnterState();
        timer = 0;
        isDamageDealt = false;
        isParticlePlayed = false;
        isAudioPlayed = false;
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        if (timer > AudioTimer && !isAudioPlayed)
        {
            AudioClip.Play();
            isAudioPlayed = true;
        }
        timer += Time.deltaTime;
        if (Particles != null && !isParticlePlayed && timer > ParticleDelay)
        {
            Particles.Play();
            isParticlePlayed = true;
        }
        Machine.currentAcceleration = Mathf.Lerp(Machine.currentAcceleration, movementCurve.Evaluate(timer) * Machine.currentAcceleration, movementSpeed * Time.deltaTime);
        Machine.Controller.Move(Machine.lastDirection * Machine.currentAcceleration * Time.deltaTime);
        if (timer > damageDelay && !isDamageDealt && !Machine.isRangedAttack)
        {
            AttackMob(DamageValue);
            isDamageDealt = true;
        }
        if (timer > duration)
        {
            if (!Machine.isRangedAttack)
            {
                if (Machine.LightAttack && nextAttack != null && !Machine.isHeavyAttack)
                {
                    Machine.SwitchState(nextAttack);
                    return;
                }
                else if (Machine.HeavyAttack && nextAttack != null && Machine.isHeavyAttack)
                {
                    Machine.SwitchState(nextAttack);
                    return;
                }
                else if (input.magnitude > 0 && Machine.isHeavyAttack)
                {
                    Machine.SwitchState(Machine.walkState);
                    //Machine.Animator.SetTrigger("isHeavyAttack");
                    return;
                }
                else if (input.magnitude == 0 && Machine.isHeavyAttack)
                {
                    Machine.SwitchState(Machine.idleState);
                    //Machine.Animator.SetTrigger("isHeavyAttack");
                    return;
                }
                else if (input.magnitude > 0)
                {
                    Machine.SwitchState(Machine.walkState);
                    return;
                }
                else if (input.magnitude == 0)
                {
                    Machine.SwitchState(Machine.idleState);
                    return;
                }
            }
            else
            {
                if (input.magnitude > 0)
                {
                    Machine.SwitchState(Machine.walkState);
                    Machine.Animator.SetTrigger("isRangedAttack");
                    return;
                }
                else if (input.magnitude == 0)
                {
                    Machine.SwitchState(Machine.idleState);
                    Machine.Animator.SetTrigger("isRangedAttack");
                    return;
                }
            }

        }
    }

    public override void OnExitState() { }

    public void AttackMobRanged(float damage)
    {
        Machine.SetCoins();
        Vector3 sphereCenter = Machine.Controller.gameObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(sphereCenter, attackRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "smallMob")
            {
                if (Machine.isPowerUped)
                {
                    collider.GetComponentInParent<MobHealth>().TakeDamage(damage*2f);
                }
                else
                {
                    collider.GetComponentInParent<MobHealth>().TakeDamage(damage);
                }
            }
        }
    }

    public void AttackMob(float damage)
    {
        Machine.SetCoins();
        Vector3 sphereCenter = Machine.Controller.gameObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(sphereCenter, attackRadius);
        foreach (Collider collider in colliders)
        {
            
            if (collider.tag == "smallMob")
            {
                Vector3 directionToCollider = (collider.transform.position - sphereCenter).normalized;
                float angleToCollider = Vector3.Angle(Machine.Controller.gameObject.transform.forward, directionToCollider);
                if (angleToCollider <= attackAngle)
                {
                    Machine.heavyAttack1.mobMachine = collider.gameObject.GetComponentInParent<MobStateMachine>();
                    if (Machine.isPowerUped)
                    {
                        collider.GetComponentInParent<MobHealth>().TakeDamage(damage * 2f);
                    }
                    else
                    {
                        collider.GetComponentInParent<MobHealth>().TakeDamage(damage);
                    }
                }
            }
        }
    }
}
