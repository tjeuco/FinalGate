using UnityEngine;

public class GruntStateReturn : GruntStateBase //// move ve vi tri ban dau
{
    public GruntStateReturn(EnemyGrunt enemyGrunt) : base(enemyGrunt)
    {
    }

    public override void Execute()
    {
        if (Mathf.Abs(this.enemyGrunt.transform.position.x - this.enemyGrunt.StartPos.x) > 0.1f)
        {
            if (this.enemyGrunt.transform.position.x > this.enemyGrunt.StartPos.x)
            {
                this.enemyGrunt.transform.localScale = new Vector3(1, 1, 1);
                this.enemyGrunt.transform.Translate(Vector3.right * this.enemyGrunt.GruntEnemyDataSO.speedGrunt * Time.deltaTime);
            }
            else
            {
                this.enemyGrunt.transform.localScale = new Vector3(-1, 1, 1);
                this.enemyGrunt.transform.Translate(Vector3.left * this.enemyGrunt.GruntEnemyDataSO.speedGrunt * Time.deltaTime);
            }
        } 
        else
        {
            this.enemyGrunt.ChangState(typeof(GruntStatePatrol));
        }    
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
