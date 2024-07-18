using UnityEngine;
using DG.Tweening;

public abstract class PotionsBase : MonoBehaviour, IPickable
{
    public PlayerStateMachine Machine;
    public float DestroyDuration;
    public Inventory PlayerInventory;
    protected bool isPicked;
    protected float timer;
    private bool startDestroying;

    protected virtual void Start()
    {
        isPicked = false;
        timer = 0;
        startDestroying = false;
        Machine = FindFirstObjectByType<PlayerStateMachine>();
        PlayerInventory = FindFirstObjectByType<Inventory>();
    }
    protected virtual void Update()
    {
        if (Machine.Pickup)
        {
            Machine.Pickup = false;
            Vector3 sphereCenter = gameObject.transform.position;
            Collider[] colliders = Physics.OverlapSphere(sphereCenter, 1f);
            Debug.Log(gameObject);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Player" && !isPicked)
                {
                    PickUp();
                    isPicked = true;
                    startDestroying = true;
                }
            }
        }
        if (startDestroying)
        {
            timer += Time.deltaTime;
            if (timer > DestroyDuration)
            {
                transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            }
        }
    }
    public virtual void PickUp()
    {
        Machine.SwitchState(Machine.pickupState);
    }
}
