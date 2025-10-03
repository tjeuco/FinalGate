using UnityEngine;

public class GruntStateReturn : GruntStateBase //// move ve vi tri ban dau
{
    public GruntStateReturn(EnemyGrunt enemyGrunt) : base(enemyGrunt)
    {
    }

    public override void Execute()
    {
        Debug.Log("Execute Return State");
        if (!this.enemyGrunt.SeePlayer() && Mathf.Abs(this.enemyGrunt.transform.position.x - this.enemyGrunt.StartPos.x) > 0.1f) // khong nhin thay player quay ve vi tri ban dau va patrol
        {
            var direction = (this.enemyGrunt.StartPos - this.enemyGrunt.transform.position).normalized;
            this.enemyGrunt.Rg.linearVelocity = direction * this.enemyGrunt.GruntEnemyDataSO.speedGrunt;

            float face = 1f;
            if (this.enemyGrunt.StartPos.x > this.enemyGrunt.transform.position.x)
            {
                face = 1f;
            }
            else
            {
                face = -1f;
            }
            this.enemyGrunt.transform.localScale = new Vector3(face, 1, 1);
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
