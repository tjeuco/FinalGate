using System.Collections;
using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;

public class EnemyGruntSpawner : MonoBehaviour
{
    [SerializeField] public  List<Transform> positions;
    [SerializeField] private int maxEnemy = 6;
    [SerializeField] private GameObject gruntPrefab;
    public int currentEnemy = 0;

    /////// tinh gio sinh enemy
    [SerializeField] private float timeDelay = 5f;
    private float timer = 0f;

    ///////////////////////////////

    void Start()
    {
        currentEnemy = 0;
        timer  = 0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (positions.Count == 0)
        {
            return;
        }
        for (int i = 0;i<positions.Count;i++)// xoa vi tri sinh enemy da destroy
        {
            if (positions[i] == null)
            {
                positions.RemoveAt(i);
            }
        }
        SpawnerEnemyGrunt();
    }
    private void SpawnerEnemyGrunt()
    {
       if (positions.Count == 0)
            {
                return;
            }
        if (timer >= timeDelay)
        {
            if (currentEnemy < maxEnemy)
            {
                Transform spawnerPoint = positions[Random.Range(0, positions.Count)];
                GameObject enemy = LazyPooling.Instance.GetObject(gruntPrefab);
                enemy.transform.position = spawnerPoint.position;
                enemy.GetComponent<HealthManager>().SetCurrentHpFull();
                enemy.GetComponent<EnemyGrunt>().SetStartPos(spawnerPoint.position);
                enemy.gameObject.SetActive(true);
                currentEnemy++;
                timer = 0f;
            }
        }
        
    }
}
