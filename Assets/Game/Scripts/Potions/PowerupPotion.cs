public class PowerupPotion : PotionsBase
{
    public override void PickUp()
    {
        base.PickUp();
        PlayerInventory.PickupPowerupPotion();
    }
}
