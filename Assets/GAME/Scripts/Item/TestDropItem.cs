using System.Collections.Generic;
using UnityEngine;

public class TestDropItem : MonoBehaviour
{
    [System.Serializable]
    public struct DropData
    {
        public GameObject itemPrefab;
        [Range(0, 100)] public float dropRate;
    }

    [SerializeField] private List<DropData> itemDrops;

    public void SpawnItem(Vector3 position)
    {
        GameObject item = GetRandomItem();
        if (item != null)
        {
            Instantiate(item, position, Quaternion.identity);
        }
    }

    private GameObject GetRandomItem()
    {
        float total = 0;
        foreach (var drop in itemDrops)
            total += drop.dropRate;

        float randomValue = Random.Range(0, total);
        float current = 0;

        foreach (var drop in itemDrops)
        {
            current += drop.dropRate;
            if (randomValue <= current)
            {
                return drop.itemPrefab;
            }
        }

        return null;
    }
}
