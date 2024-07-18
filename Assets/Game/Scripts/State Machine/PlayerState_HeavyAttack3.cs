using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class PlayerState_HeavyAttack3 : BasePlayerAttackState
{
    public float jumpForce;
    public float jumpSpeed;
    private float currentJumpForce;
    private bool damageDealt;
    private float endTimer;
    public MobStateMachine mobMachine;


    public override void OnEnterState()
    {
        base.OnEnterState();
        damageDealt = false;
        currentJumpForce = jumpForce;
        if (!Machine.HeavyAttack)
        {
            Machine.Animator.SetTrigger("isHeavyAttack");
        }
        Machine.Animator.SetInteger("HeavyAttackNum", 2);
        endTimer = 0;
        isParticlePlayed = false;
    }

    public override void OnUpdateState()
    {
        //base.OnUpdateState();
        if (endTimer > AudioTimer && !isAudioPlayed)
        {
            AudioClip.Play();
            isAudioPlayed = true;
        }
        currentJumpForce -= Time.deltaTime * jumpSpeed;
        Vector3 motion = Vector3.up * currentJumpForce * Time.deltaTime;
        Machine.Controller.Move(motion);
        endTimer += Time.deltaTime;
        if (Particles != null && !isParticlePlayed && endTimer > ParticleDelay)
        {
            Particles.Play();
            isParticlePlayed = true;
        }
        if (mobMachine != null && mobMachine.gameObject.activeSelf == true)
        {
            if (currentJumpForce <= 0 && !damageDealt && mobMachine != null && mobMachine.gameObject.activeSelf == true)
            {
                mobMachine.takeDamage.isFalling = true;
                mobMachine.MobHealth.isKnockedDown = true;
                AttackMob(20f);
                mobMachine.mobAnimator.SetBool("isHitFalling", true);
                damageDealt = true;
            }
            if (currentJumpForce < -jumpForce)
            {
                if (mobMachine.MobHealth.health <= 0)
                {
                    mobMachine.SwitchState(mobMachine.deathState);
                }
                Machine.SwitchState(Machine.idleState);
            }
        }
        
        if (endTimer > duration)
        {
            Machine.SwitchState(Machine.idleState);
        }
    }

    public override void OnExitState()
    {
        if (mobMachine != null)
        {
            mobMachine.takeDamage.isFalling = false;
        }
        Machine.Animator.SetTrigger("isHeavyAttack");
    }
}