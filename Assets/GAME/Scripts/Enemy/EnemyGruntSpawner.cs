using UnityEngine;
using System.Collections.Generic;

public class EnemyGruntSpawner : Item, IHitable
{
    [SerializeField] private int maxEnemy = 6;
    [SerializeField] private GameObject gruntPrefab;
    [SerializeField] Transform positionEnemy;
    private List<EnemyGrunt> listEnemyActives = new List<EnemyGrunt>();

    public int currentEnemy = 0;

    /////// tinh gio sinh enemy
    [SerializeField] private float timeDelay = 5f;
    private float timer = 0f;

    ///////////////////////////////

    protected override void Start()
    {
        base.Start();
        this.currentEnemy = 0;
        this.timer  = 0f;
    }
    private void Update()
    {
        this.timer += Time.deltaTime;

        if (this.timer >= this.timeDelay)
        {
            SpawnerEnemyGrunt();
            this.timer = 0f;
        }
    }
    public void SpawnerEnemyGrunt()
    {
        if (currentEnemy < maxEnemy)
            {
                EnemyGrunt enemy = LazyPooling.Instance.GetObject(gruntPrefab).GetComponent<EnemyGrunt>();
                enemy.transform.position = this.positionEnemy.position;
                enemy.GetComponent<HealthManager>().SetCurrentHpFull();
                enemy.GetComponent<HealthManager>().SetSpawerGate(this);
                enemy.SetStartPos(this.positionEnemy.position);
                this.listEnemyActives.Add(enemy);
                enemy.gameObject.SetActive(true);
                currentEnemy++;
                timer = 0f;
            }
    }

    public void NotifyEnemyDie(EnemyGrunt enemy)
    {
        this.currentEnemy--;    
        this.listEnemyActives.Remove(enemy);
    }
}
