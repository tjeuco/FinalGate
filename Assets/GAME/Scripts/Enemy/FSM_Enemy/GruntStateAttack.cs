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

        // Enemy dduoir theo player

        if (this.enemyGrunt.PlayerPosition.transform.position.x > this.enemyGrunt.transform.position.x)
        {
            this.enemyGrunt.transform.localScale = new Vector3(1, 1, 1);
            this.enemyGrunt.transform.Translate(Vector3.right * this.enemyGrunt.GruntEnemyDataSO.speedGrunt * Time.deltaTime);
        }
        else
        {
            this.enemyGrunt.transform.localScale = new Vector3(-1, 1, 1);
            this.enemyGrunt.transform.Translate(Vector3.left * this.enemyGrunt.GruntEnemyDataSO.speedGrunt * Time.deltaTime);
        }
        /////// shoooooot....
        if (fireCoolDown <= 0f)
        {
            fireCoolDown = this.enemyGrunt.GruntEnemyDataSO.fireRate;
            GameObject bullet = LazyPooling.Instance.GetObject(this.enemyGrunt.GruntEnemyDataSO.bulletGruntPrefab);
            bullet.transform.position = this.enemyGrunt.FirePointGrunt.position;
            bullet.gameObject.SetActive(true);
            GruntBullet gruntBullet = bullet.GetComponent<GruntBullet>();
            gruntBullet.SetDirectionBullet(this.enemyGrunt.transform.localScale.x > 0 ? Vector3.right : Vector3.left);
        }

        if (Vector3.Distance(this.enemyGrunt.transform.position, this.enemyGrunt.PlayerPosition.transform.position) > this.enemyGrunt.GruntEnemyDataSO.distanceDetectPlayer)
        {
            this.enemyGrunt.ChangState(typeof(GruntStateReturn));
        }

    }

    public override void OnEnter()
    {
        //Debug.Log("Entering Attack State");
        //fireCoolDown = 0;
    }

    public override void OnExit()
    {
        //Debug.Log("Exiting Attack State");
    }
}
