using UnityEngine;
using System.Collections.Generic;

public class EnemyGruntSpawner : Item, IHitable
{
    [SerializeField] private int maxEnemy = 6;
    [SerializeField] private GameObject gruntPrefab;
    public int currentEnemy = 0;

    /////// tinh gio sinh enemy
    [SerializeField] private float timeDelay = 5f;
    private float timer = 0f;

    ///////////////////////////////

    protected override void Start()
    {
        base.Start();
        currentEnemy = 0;
        timer  = 0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        SpawnerEnemyGrunt();
    }
    private void SpawnerEnemyGrunt()
    {


        if (timer >= timeDelay)
        {
            if (currentEnemy < maxEnemy)
            {
                GameObject enemy = LazyPooling.Instance.GetObject(gruntPrefab);
                enemy.transform.position = this.transform.position;
                enemy.GetComponent<HealthManager>().SetCurrentHpFull();
                enemy.GetComponent<EnemyGrunt>().SetStartPos(this.transform.position);
                enemy.gameObject.SetActive(true);
                currentEnemy++;
                timer = 0f;
            }
        }
        
    }
}
