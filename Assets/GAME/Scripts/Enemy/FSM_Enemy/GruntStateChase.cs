using UnityEngine;

public class GruntStateChase : GruntStateBase
{
    public GruntStateChase(EnemyGrunt enemyGrunt) : base(enemyGrunt)
    {
    }

    public override void Execute()
    {
        Debug.Log("Chasing the player!");
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Chase State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Chase State");
    }
}
