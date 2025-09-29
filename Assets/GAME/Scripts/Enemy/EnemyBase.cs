using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBase:MonoBehaviour
{
    [SerializeField] protected float speedGrunt = 0.5f;
    [SerializeField] protected float distance = 3f;
    protected bool moveRight = true;
    protected Rigidbody2D rg;
    protected Vector3 startPos;
    private PlayerControl playerControl;
    private BulletPlayer bulletPlayer;
    private float maxHp = 100f;
    private float currentHp;

    protected virtual void Awake()
    {
        playerControl = FindAnyObjectByType<PlayerControl>();
        rg = GetComponent<Rigidbody2D>();
        startPos = this.transform.position;
        currentHp = maxHp;
    }

    protected virtual void Update()
    {
        EnemyMove();
    }
    protected virtual void EnemyMove()
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
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) {  return; }
        if (collision.CompareTag("PlayerBullet")) 
        {
            BulletPlayer bullet = collision.GetComponent<BulletPlayer>();
            this.TakeDamage(bullet.GetDamageBulletPlayer());
        }
    }
    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        if (currentHp<=0)
        {
            Die();
        }
    }
    protected virtual void Attack()
    {

    }
    protected virtual void Die()
    {
        this.gameObject.SetActive(false);
        EnemyGruntSpawner numberEnemy = FindAnyObjectByType<EnemyGruntSpawner>();
        numberEnemy.currentEnemy--;
    }
    
    public void SetCurrentHpFull()
    {
        currentHp = maxHp;
    }*/
    public void SetStartPos( Vector3 pos) // set lai pos khi pooling enemy
    {
        startPos = pos;
    }
}
