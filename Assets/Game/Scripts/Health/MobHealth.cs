using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MobHealth : HealthBase
{
    public MobStateMachine Machine;
    public bool isKnockedUp;
    public bool isKnockedDown;
    protected override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if(isKnockedUp)
        {
            Machine.SwitchState(Machine.knockupDamage);
            return;
        }
        else if (!isKnockedDown)
        {
            Machine.SwitchState(Machine.takeDamage);
            return;
        }

        isKnockedDown = false;

    }
}
