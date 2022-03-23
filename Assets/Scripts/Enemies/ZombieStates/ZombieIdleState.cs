using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : ZombieStates
{
    int movementZhash = Animator.StringToHash("MovementZ");

    public ZombieIdleState(ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {

    }

    public override void Start()
    {
        base.Start();
        ownerZombie.zombieNavMeshAgent.isStopped = true;
        ownerZombie.zombieNavMeshAgent.ResetPath();
        ownerZombie.zombieAnimator.SetFloat(movementZhash, 0);
    }

    public override void Exit()
    {
        base.Exit();
        ownerZombie.zombieNavMeshAgent.isStopped = false;
    }
}
