using UnityEngine;

public class GruntStateAttack : GruntStateBase
{
    private float fireCoolDown;
    private Rigidbody2D rg;
    public GruntStateAttack(EnemyGrunt enemyGrunt) : base(enemyGrunt)
    {
    }

    public override void Execute()
    {
        
        fireCoolDown -= Time.deltaTime;

        // Enemy dduoir theo player
        float face = 1f;
        if (this.enemyGrunt.PlayerPosition.transform.position.x > this.enemyGrunt.transform.position.x)
        {
            face = 1f;
        }
        else
        {
            face = -1f;
        }
        this.enemyGrunt.transform.localScale = new Vector3(face, 1, 1);

        if (!this.enemyGrunt.SeePlayer()) // ko thay player thi quay ve vi tri ban dau
        {
            this.enemyGrunt.ChangState(typeof(GruntStateReturn));
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

    }

    public override void OnEnter()
    {
        Debug.Log("Entering Attack State");
        fireCoolDown = this.enemyGrunt.GruntEnemyDataSO.fireRate;
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Attack State");
    }
}
