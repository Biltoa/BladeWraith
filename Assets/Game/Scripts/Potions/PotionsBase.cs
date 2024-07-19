using UnityEngine;
using DG.Tweening;

public abstract class PotionsBase : MonoBehaviour, IPickable
{
    public float DestroyDuration;
    protected float timer;
    private bool startDestroying;

    protected virtual void Start()
    {
        timer = 0;
        startDestroying = false;
    }
    protected virtual void Update()
    {
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
        startDestroying = true;
    }
}
