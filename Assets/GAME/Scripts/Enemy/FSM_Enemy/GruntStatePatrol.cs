using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GruntStatePatrol : GruntStateBase
{
    private bool moveRight = true;

    public GruntStatePatrol(EnemyGrunt enemyGrunt) : base(enemyGrunt)
    {
    }

    public override void Execute()
    {
        if (this.enemyGrunt.PlayerPosition == null)
        {
            return;
        }

        float leftBound = this.enemyGrunt.StartPos.x - this.enemyGrunt.GruntEnemyDataSO.distancePatrol;
        float rightBound = this.enemyGrunt.StartPos.x + this.enemyGrunt.GruntEnemyDataSO.distancePatrol;
        if (moveRight)
        {
            this.enemyGrunt.transform.Translate(Vector3.right * this.enemyGrunt.GruntEnemyDataSO.speedGrunt * Time.deltaTime);
            if (this.enemyGrunt.transform.position.x >= rightBound)
            {
                moveRight = false;
                this.enemyGrunt.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            this.enemyGrunt.transform.Translate(Vector3.left * this.enemyGrunt.GruntEnemyDataSO.speedGrunt * Time.deltaTime);
            if (this.enemyGrunt.transform.position.x <= leftBound)
            {
                moveRight = true;
                this.enemyGrunt.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (Vector3.Distance(this.enemyGrunt.transform.position, this.enemyGrunt.PlayerPosition.transform.position) <= this.enemyGrunt.GruntEnemyDataSO.distanceDetectPlayer)
        {
            //Attack();
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
