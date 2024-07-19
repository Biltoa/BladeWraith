using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerState_Pickup : PlayerStateBase
{
    private float timer;
    public float Duration;
    public LayerMask PotionLayerMask;

    public override void OnEnterState()
    {
        timer = 0;
        Vector3 sphereCenter = Machine.Controller.gameObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(sphereCenter, 2f, PotionLayerMask);
        PotionsBase nearestPotion = null;
        float nearestDistance = float.MaxValue;
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Potion")
            {
                if (Vector3.Distance(sphereCenter, collider.transform.position) < nearestDistance)
                {
                    nearestDistance = Vector3.Distance(sphereCenter, collider.transform.position);
                    nearestPotion = collider.GetComponent<PotionsBase>();
                }
            }
        }
        Machine.Animator.SetBool("isPicking", true);
        if (nearestPotion.GetComponent<HealPotion>() != null)
        {
            Machine.PlayerInventory.PickupHealPotion();
        }
        else
        {
            Machine.PlayerInventory.PickupPowerupPotion();
        }
        nearestPotion.GetComponent<PotionsBase>().PickUp();
        
    }

    public override void OnUpdateState()
    {
        base.OnUpdateState();
        timer += Time.deltaTime;
        if (timer > Duration)
        {
            Machine.SwitchState(Machine.idleState);
            return;
        }
    }

    public override bool CanEnterState()
    {
        Vector3 sphereCenter = Machine.Controller.gameObject.transform.position;
        Collider[] colliders = Physics.OverlapSphere(sphereCenter, 2f, PotionLayerMask);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Potion")
            {
                return true;
            }
        }
        return false;
    }

    public override void OnExitState()
    {
        Machine.Animator.SetBool("isPicking", false);
    }
}
