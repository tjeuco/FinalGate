using System.Collections.Generic;
using UnityEngine;

public class LazyPooling : Singleton<LazyPooling>
{
    Dictionary<GameObject,List<GameObject>> pools = new Dictionary<GameObject,List<GameObject>>();

    public GameObject GetObject(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
        {
            this.pools.Add(prefab, new List<GameObject>());
        }
        foreach (var item in this.pools[prefab])
        {
            if (item.activeSelf)
                continue;
            return item;
        }
        GameObject g = Instantiate(prefab,this.transform);
        this.pools[prefab].Add(g);
        g.SetActive(false);
        return g;
    }

    public void ReturnObject(GameObject prefab)
    {
        prefab.gameObject.SetActive(false);
    }

    public T GetObjectType<T>(T prefab) where T : MonoBehaviour
    {
        return this.GetObject(prefab.gameObject).GetComponent<T>();
    }
}
