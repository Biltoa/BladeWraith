public class HealPotion : PotionsBase
{
    public override void PickUp()
    {
        base.PickUp();
        PlayerInventory.PickupHealPotion();
    }
}