using UnityEngine;

public class GruntStatePatrol : GruntStateBase
{
    public GruntStatePatrol(EnemyGrunt enemyGrunt) : base(enemyGrunt)
    {
    }

    public override void Execute()
    {
        Debug.Log("Patrolling the area.");
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Patrol State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Patrol State");
    }
}
