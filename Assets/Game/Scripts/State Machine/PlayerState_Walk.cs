
using UnityEngine;

public class PlayerState_Walk : PlayerStateBase
{
    private Quaternion targetRotation;
    public Inventory PlayerInventory;
    public FloatingJoystick Joystick;
    public float FootstepSpeed = 8.0f;
    
    public override void OnEnterState()
    {
        Machine.Footsteps.Play();
        Machine.isHeavyAttack = false;
        Machine.isLightAttack = false;
        Machine.Animator.SetBool("Reset", true);
    }

    public override void OnExitState()
    {
        Machine.Footsteps.Stop();
        Machine.Animator.SetBool("Reset", false);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        //input = new Vector2(Joystick.Horizontal, Joystick.Vertical);
        if (Machine.Footsteps.pitch < 0.2f)
        {
            Machine.Footsteps.volume = 0;
        }
        else
        {
            Machine.Footsteps.volume = 0.05f;
        }
        if (Machine.currentAcceleration != 0)
        {
            Machine.Footsteps.pitch = Machine.currentAcceleration / FootstepSpeed;
        }
        
        if (input.magnitude != 0)
            Machine.lastDirection = new Vector3(input.x, 0, input.y).normalized;
        if (input.magnitude > 0)
            Machine.currentAcceleration = Mathf.MoveTowards(Machine.currentAcceleration, Machine.speed * input.normalized.magnitude, Time.deltaTime*Machine.acceleration);
        else
            Machine.currentAcceleration = Mathf.MoveTowards(Machine.currentAcceleration, Machine.speed * input.normalized.magnitude, Time.deltaTime * Machine.deAcceleration);
        targetRotation = Quaternion.LookRotation(Machine.lastDirection);
        Machine.Controller.transform.rotation = Quaternion.Lerp(Machine.Controller.transform.rotation, targetRotation, Machine.rotationSpeed * Time.deltaTime);
        Machine.Controller.Move(Machine.lastDirection * Machine.currentAcceleration * Time.deltaTime);

        if (Machine.currentAcceleration <= 0)
        {
            Machine.SwitchState(Machine.idleState); 
        }

        if (Machine.SwitchAttack)
        {
            Machine.isRangedAttack = !Machine.isRangedAttack;
            SwitchAttackMode();
            Machine.SwitchAttack = false;
        }

        if (Machine.Roll)
        {
            Machine.Roll = false;
            Machine.SwitchState(Machine.rollState);
        }

        if (Machine.Block)
        {
            Machine.SwitchState(Machine.blockState);
        }

        if (Machine.Parry)
        {
            Machine.Parry = false;
            Machine.SwitchState(Machine.parryState);
        }

        if (Machine.LightAttack && !Machine.isRangedAttack)
        {
            Machine.SwitchState(Machine.lightAttack1);
            Machine.isHeavyAttack = false;
        }

        if (Machine.HeavyAttack && !Machine.isRangedAttack)
        {
            Machine.SwitchState(Machine.heavyAttack1);
            Machine.isHeavyAttack = true;
        }

        if (Machine.LightAttack && Machine.isRangedAttack)
        {
            Machine.SwitchState(Machine.rangedAttack1);
            Machine.isRangedAttack = true;
        }

        if (Machine.HeavyAttack && Machine.isRangedAttack)
        {
            Machine.SwitchState(Machine.rangedAttack2);
            Machine.isRangedAttack = true;
        }
    }
}
