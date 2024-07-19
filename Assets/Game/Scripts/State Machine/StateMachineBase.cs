using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class StateMachineBase : MonoBehaviour
{
    public StateBase currentState;
    
    protected abstract void SetInitialState();

    private void Start()
    {
        SetInitialState();
    }

    protected virtual void Update()
    {
        currentState.OnUpdateState();
    }

    public void SwitchState(StateBase state)
    {
        currentState.OnExitState();
        currentState = state;
        currentState.OnEnterState();

    }
}
