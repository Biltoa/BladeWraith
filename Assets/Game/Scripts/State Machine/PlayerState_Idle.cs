using UnityEngine;

public class PlayerState_Idle : PlayerStateBase
{
    public Inventory PlayerInventory;
    public FloatingJoystick Joystick;
    public bool IsTakingDamage;
    public override void OnEnterState()
    {
        Machine.isHeavyAttack = false;
        Machine.isLightAttack = false;
        Machine.Animator.SetBool("Reset", true);

    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("Reset", false);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        input = new Vector2(Joystick.Horizontal, Joystick.Vertical);
        Machine.Controller.Move(Vector3.zero);
        if (input.magnitude > 0)
        {
            Machine.SwitchState(Machine.walkState);
            return;
        }

        if (Machine.DrinkPower && PlayerInventory.PowerupPotions > 0)
        {
            Machine.DrinkPower = false;
            Machine.drinkPotion.PotionType = PotionType.Powerup;
            Machine.SwitchState(Machine.drinkPotion);
            return;
        }

        if (Machine.DrinkHealth && PlayerInventory.HealPotions > 0)
        {
            Machine.DrinkHealth = false;
            Machine.drinkPotion.PotionType = PotionType.Health;
            Machine.SwitchState(Machine.drinkPotion);
            return;
        }

        if (Machine.SwitchAttack)
        {
            Machine.isRangedAttack = !Machine.isRangedAttack;
            SwitchAttackMode();
            Machine.SwitchAttack = false;
        }

        if (Machine.Roll)
        {
            Machine.SwitchState(Machine.rollState);
            return;
        }

        if (Machine.Block)
        {
            Machine.SwitchState(Machine.blockState);
            return;
        }

        if (Machine.Parry)
        {
            Machine.SwitchState(Machine.parryState);
            return;
        }

        if (Machine.Pickup)
        {
            Machine.Pickup = false;
            if (Machine.pickupState.CanEnterState())
            {
                Machine.SwitchState(Machine.pickupState);
                return;
            }
        }

        if (Machine.LightAttack && !Machine.isRangedAttack && !IsTakingDamage)
        {
            Machine.SwitchState(Machine.lightAttack1);
            Machine.isHeavyAttack = false;
            return;
        }

        if (Machine.HeavyAttack && !Machine.isRangedAttack && !IsTakingDamage)
        {
            Machine.SwitchState(Machine.heavyAttack1);
            Machine.isHeavyAttack = true;
            return;
        }

        if (Machine.LightAttack && Machine.isRangedAttack && !IsTakingDamage)
        {
            Machine.SwitchState(Machine.rangedAttack1);
            Machine.isRangedAttack = true;
            return;
        }

        if (Machine.HeavyAttack && Machine.isRangedAttack && !IsTakingDamage)
        {
            Machine.SwitchState(Machine.rangedAttack2);
            Machine.isRangedAttack = true;
            return;
        }

    }
}
