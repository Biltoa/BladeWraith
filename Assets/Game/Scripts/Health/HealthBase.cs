using System.Collections;
using UnityEngine;

public abstract class HealthBase : MonoBehaviour
{
    public float health;

    protected virtual void Start()
    {
        health = 100;
    }

    public virtual void Heal(float amount)
    {
        if (health > 0)
        {
            health += amount;
        }
        if (health > 100)
        {
            health = 100;
        }
    }

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
    }
}
