using UnityEngine;

public class GruntStateAttack : GruntStateBase
{
    private float fireCoolDown;
    public GruntStateAttack(EnemyGrunt enemyGrunt) : base(enemyGrunt)
    {
    }

    public override void Execute()
    {
        Debug.Log("Execute Attack Sate");
        fireCoolDown -= Time.deltaTime;

        if (fireCoolDown <= 0f)
        {
            fireCoolDown = this.enemyGrunt.GruntEnemyDataSO.fireRate;
            GameObject bullet = LazyPooling.Instance.GetObject(this.enemyGrunt.GruntEnemyDataSO.bulletGruntPrefab);
            bullet.transform.position = this.enemyGrunt.FirePointGrunt.position;
            bullet.gameObject.SetActive(true);
            GruntBullet gruntBullet = bullet.GetComponent<GruntBullet>();
            gruntBullet.SetDirectionBullet(this.enemyGrunt.transform.localScale.x > 0 ? Vector3.right : Vector3.left);
        }

        if (Vector3.Distance(this.enemyGrunt.transform.position, this.enemyGrunt.PlayerPosition.transform.position) > this.enemyGrunt.GruntEnemyDataSO.distanceAttackPlayer)
        {
            this.enemyGrunt.ChangState(typeof(GruntStatePatrol));
        }

        Debug.Log(Vector3.Distance(this.enemyGrunt.transform.position, this.enemyGrunt.PlayerPosition.transform.position));
    }

    public override void OnEnter()
    {
        //Debug.Log("Entering Attack State");
        //fireCoolDown = 0;
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Attack State");
    }
}
