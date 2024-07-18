using UnityEngine;

public abstract class PlayerStateBase : StateBase
{
    public PlayerStateMachine Machine;


    protected Vector2 input;
    private Vector3 tempInput;


    public override void OnEnterState() 
    {
        Machine.Animator.SetBool("Reset", true);
        Machine.Animator.SetBool("Reset", false);
    }

    public override void OnUpdateState()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        tempInput = Quaternion.Euler(0, 45, 0) * new Vector3(input.x, 0, input.y);
        input = new Vector2(tempInput.x, tempInput.z);
        Machine.Animator.SetFloat("Speed", Machine.Controller.velocity.magnitude/Machine.speed);
    }

    public void SwitchAttackMode()
    {
    }

    public override void OnExitState() { } 

    public override bool CanEnterState() { return true; }
}
