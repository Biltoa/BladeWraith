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
        Machine.Animator.SetBool("isHeavyAttack3", true);
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
            ParticleSystem temp = Instantiate(Particles, Vector3.zero, Quaternion.identity, ParticleParent.transform);
            temp.transform.localPosition = Vector3.zero;
            temp.transform.rotation = ParticleParent.transform.rotation;
            temp.transform.SetParent(null);
            temp.Play();
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
                return;
            }
        }
        
        if (endTimer > duration)
        {
            Machine.SwitchState(Machine.idleState);
            return;
        }
    }

    public override void OnExitState()
    {
        if (mobMachine != null)
        {
            mobMachine.takeDamage.isFalling = false;
        }
        Machine.Animator.SetBool("isHeavyAttack3", false);
    }
}