using UnityEngine;

public class PlayerState_RangedAttack1 : BasePlayerAttackState
{
    public GameObject SpellPrefab;
    public GameObject FirePoint;
    public float AttackDelay;
    private bool hasAttacked;
    public override void OnEnterState()
    {
        base.OnEnterState();
        Machine.Animator.SetTrigger("isRangedAttack");
        Machine.Animator.SetInteger("RangedAttackNum", 0);
        timer = 0;
        hasAttacked = false; 
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        Machine.Controller.Move(Vector3.zero);
        timer += Time.deltaTime;
        if (timer > AttackDelay && !hasAttacked)
        {
            Instantiate(SpellPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            hasAttacked = true;
        }
    }
}
