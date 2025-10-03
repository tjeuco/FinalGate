using UnityEngine;

public class GruntStatePatrol : GruntStateBase
{
    private bool moveRight = true;

    public GruntStatePatrol(EnemyGrunt enemyGrunt) : base(enemyGrunt)
    {
    }

    public override void Execute()
    {
        Debug.Log("Execute Patrol State");

        if (this.enemyGrunt.PlayerPosition == null)
        {
            return;
        }

        float leftBound = this.enemyGrunt.StartPos.x - this.enemyGrunt.GruntEnemyDataSO.distancePatrol;
        float rightBound = this.enemyGrunt.StartPos.x + this.enemyGrunt.GruntEnemyDataSO.distancePatrol;
        if (moveRight)
        {
            this.enemyGrunt.Rg.linearVelocity = Vector3.right * this.enemyGrunt.GruntEnemyDataSO.speedGrunt ;
            if (this.enemyGrunt.transform.position.x >= rightBound)
            {
                moveRight = false;
                this.enemyGrunt.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            this.enemyGrunt.Rg.linearVelocity = Vector3.left * this.enemyGrunt.GruntEnemyDataSO.speedGrunt;
            if (this.enemyGrunt.transform.position.x <= leftBound)
            {
                moveRight = true;
                this.enemyGrunt.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (this.enemyGrunt.SeePlayer())
        {
            this.enemyGrunt.ChangState(typeof(GruntStateAttack));
        }
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
