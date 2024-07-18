using UnityEngine;

public class PlayerState_Idle : PlayerStateBase
{
    public Inventory PlayerInventory;
    public FloatingJoystick Joystick;
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
        }

        if (Machine.DrinkPower && PlayerInventory.PowerupPotions > 0)
        {
            Machine.DrinkPower = false;
            Machine.drinkPotion.PotionType = PotionType.Powerup;
            Machine.SwitchState(Machine.drinkPotion);
        }

        if (Machine.DrinkHealth && PlayerInventory.HealPotions > 0)
        {
            Machine.DrinkHealth = false;
            Machine.drinkPotion.PotionType = PotionType.Health;
            Machine.SwitchState(Machine.drinkPotion);
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
            Machine.LightAttack = false;
            Machine.SwitchState(Machine.rangedAttack1);
            Machine.isRangedAttack = true;
        }

        if (Machine.HeavyAttack && Machine.isRangedAttack)
        {
            Machine.HeavyAttack = false;
            Machine.SwitchState(Machine.rangedAttack2);
            Machine.isRangedAttack = true;
        }

    }
}
