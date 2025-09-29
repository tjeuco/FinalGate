using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class EnemyGrunt : MonoBehaviour, IHitable
{
    
    //private Animator animator;
    private float idleDelay = 2f;
    private float idleTimer;

    [SerializeField] private GameObject bulletGrunt;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float shootRange = 10f;
    [SerializeField] private float fireCoolDown;
    [SerializeField] private int scoreEnemyGrunt = 1;
    [SerializeField] protected float speedGrunt = 0.5f;
    [SerializeField] protected float distance = 3f;
    protected bool moveRight = true;

    private Vector3 startPos;
    private PlayerControl playerPos;

    EnemyStateBase currentEnemyState;

    void Awake()
    {
        fireCoolDown = 1f / fireRate;
        idleTimer = idleDelay ;
        startPos = this.transform.position;
        playerPos=FindAnyObjectByType<PlayerControl>();
    }
     void Update()
    {
        currentEnemyState.Execute(this);
        if (playerPos == null)
        {
             return;
        }
        if (idleTimer > 0)
          {
              idleTimer -= Time.deltaTime;
              return;
          }
        if (Vector3.Distance(transform.position, playerPos.transform.position) <= shootRange)
          {
              Attack();
          }
        EnemyMove();
        CheckPosEnemy();
    }

    public void ChangeState(EnemyStateBase newState)
    {
        if (currentEnemyState == newState) return;
        this.currentEnemyState.OnExit();
        this.currentEnemyState = newState;
        this.currentEnemyState.OnEnter();
    }
     void Attack()
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
    }
   
    public void CheckPosEnemy()
    {
        if (this.transform.position.y < -20f)
        {
            this.GetComponent<HealthManager>().Die();
        }
    }

    
    void EnemyMove()
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
        return scoreEnemyGrunt;
    }
}
