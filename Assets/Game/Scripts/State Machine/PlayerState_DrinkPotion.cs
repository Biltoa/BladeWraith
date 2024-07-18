using UnityEngine;
public enum PotionType
{
    Health, Powerup
}
public class PlayerState_DrinkPotion : PlayerStateBase
{
    private float timer;
    public float Duration;
    public PotionType PotionType;
    public Inventory playerInventory;
    public GameObject Sword;
    public GameObject PowerupPotion;
    public GameObject HealPotion;
    public GameObject DrinkPosition;
    private GameObject potion;

    public override void OnEnterState()
    {
        timer = 0;
        Sword.SetActive(false);
        if (PotionType == PotionType.Health)
        {
            potion = Instantiate(HealPotion, DrinkPosition.transform.position, Quaternion.identity);
            Machine.PlayerHealth.Heal(50f);
            Machine.HealingParticle.Play();
        }
        else
        {
            potion = Instantiate(PowerupPotion, DrinkPosition.transform.position, Quaternion.identity);
            Machine.powerTimer = 0;
            Machine.PowerupParticle.Play();
            Machine.PowerUp();
        }

        potion.transform.SetParent(DrinkPosition.transform);

        Machine.Animator.SetBool("isDrinking", true);
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        timer += Time.deltaTime;
        if (timer > Duration)
        {
            DrinkPotion();
            Machine.SwitchState(Machine.idleState);
        }
    }

    private void DrinkPotion()
    {
        if (PotionType == PotionType.Powerup)
        {
            playerInventory.DropPowerupPotion();
        }
        else if (PotionType == PotionType.Health)
        {
            playerInventory.DropHealPotion();
        }
    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("isDrinking", false);
        Sword.SetActive(true);
        Machine.HealingParticle.Stop();
        Destroy(potion);
    }
}