using UnityEngine;

public abstract class StateBase : MonoBehaviour
{

    public virtual void OnEnterState() { }

    public virtual void OnUpdateState() { }

    public virtual void OnExitState() { }

    public virtual bool CanEnterState() { return true; }
}