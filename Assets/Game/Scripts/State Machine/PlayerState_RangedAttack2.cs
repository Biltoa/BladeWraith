using UnityEngine;

public class PlayerState_RangedAttack2 : BasePlayerAttackState
{
    public GameObject SpellPrefab;
    public GameObject FirePoint;
    public float AttackDelay;
    private bool hasUsedSpell;
    private bool hasAttacked;
    private GameObject spellVFX;
    public AudioSource ImpactAudio;
    private bool isPlayed;
    public override void OnEnterState()
    {
        base.OnEnterState();
        Machine.Animator.SetTrigger("isRangedAttack");
        Machine.Animator.SetInteger("RangedAttackNum", 1);
        timer = 0;
        hasAttacked = false;
        hasUsedSpell = false;
        isPlayed = false;
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        Machine.Controller.Move(Vector3.zero);
        timer += Time.deltaTime;
        if (timer > 1f && !isPlayed)
        {
            ImpactAudio.Play();
            isPlayed = true;
        }
        if (!hasUsedSpell)
        {
            spellVFX = Instantiate(SpellPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            hasUsedSpell = true;
        }
        if (timer > AttackDelay && !hasAttacked)
        {
            AttackMobRanged(20f);
            hasAttacked = true;
        }
    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("isRangedAttack", false);
        Destroy(spellVFX);
        Machine.HeavyAttack = false;
    }
}