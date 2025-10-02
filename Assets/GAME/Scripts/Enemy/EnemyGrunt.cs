using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGrunt : MonoBehaviour, IHitable
{
    [SerializeField] private Transform firePointGrunt;
    public Transform FirePointGrunt => firePointGrunt;

    [SerializeField] private GruntEnemyDataSO gruntEnemyDataSO;
    public GruntEnemyDataSO GruntEnemyDataSO => gruntEnemyDataSO;
    

    private Vector3 startPos;
    public Vector3 StartPos => startPos;

    private PlayerControl playerPos;
    public PlayerControl PlayerPosition => playerPos;

    GruntStateBase currentEnemyState; // trạng thái hiện tại của enemy
    Dictionary<Type, GruntStateBase> enemyStates = new Dictionary<Type, GruntStateBase>();

    private void Awake()
    {
        this.enemyStates.Add(typeof(GruntStateAttack), new GruntStateAttack(this));
        this.enemyStates.Add(typeof(GruntStateChase), new GruntStateChase(this));
        this.enemyStates.Add(typeof(GruntStatePatrol), new GruntStatePatrol(this));
    }

    private void OnEnable()
    {
        startPos = this.transform.position;
        playerPos = FindAnyObjectByType<PlayerControl>();
        ChangState(typeof(GruntStatePatrol));
    }

    
    public void ChangState(Type newState)
    {
        if (!this.enemyStates.ContainsKey(newState))
            return;

        if (this.currentEnemyState == this.enemyStates[newState]) 
            return;

        if (this.currentEnemyState != null)
            this.currentEnemyState.OnExit();
        this.currentEnemyState = this.enemyStates[newState];
        this.currentEnemyState.OnEnter();
    }

     void Update()
    {
        currentEnemyState.Execute();
        
        //EnemyPatrol();
        CheckPosEnemy();
    }

    /* void Attack()
    {
        fireCoolDown -= Time.deltaTime;

        if (fireCoolDown <= 0f)
        {
            fireCoolDown = fireRate;
            GameObject bullet =LazyPooling.Instance.GetObject(bulletGrunt);
            bullet.transform.position= firePoint.position;
            bullet.gameObject.SetActive(true);
            GruntBullet gruntBullet = bullet.GetComponent<GruntBullet>();
            gruntBullet.SetDirectionBullet(transform.localScale.x > 0 ? Vector3.right : Vector3.left);
        }
    } */
   
    public void CheckPosEnemy()
    {
        if (this.transform.position.y < -20f)
        {
            this.GetComponent<HealthManager>().Die();
        }
    }

    
   /* void EnemyPatrol()
    {
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;
        if (moveRight)
        {
            this.transform.Translate(Vector3.right * speedGrunt * Time.deltaTime);
            if (this.transform.position.x >= rightBound)
            {
                moveRight = false;
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            this.transform.Translate(Vector3.left * speedGrunt * Time.deltaTime);
            if (this.transform.position.x <= leftBound)
            {
                moveRight = true;
                this.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
   */
    

    public void SetStartPos(Vector3 pos) // set lai pos khi pooling enemy
    {
        startPos = pos;
    }

    public void GetHit(float dmg)
    {
        this.GetComponent<HealthManager>().TakeDamage(dmg);
    }

    public int ReturnScoreEnemy()
    {
        return this.gruntEnemyDataSO.scoreEnemyGrunt;
    }
}
