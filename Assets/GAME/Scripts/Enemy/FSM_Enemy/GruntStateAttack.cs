using UnityEngine;

public class GruntStateAttack : GruntStateBase
{
    public GruntStateAttack(EnemyGrunt enemyGrunt) : base(enemyGrunt)
    {
    }

    public override void Execute()
    {
        Debug.Log("Attacking the player!");
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Attack State");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Attack State");
    }
}
