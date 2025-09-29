using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.Unicode;

public class EnemyPooling : MonoBehaviour
{
    [SerializeField] private GameObject gruntPerfabs;
    List<GameObject> grunts = new List<GameObject>();


    public GameObject GetGruntEnemy(Vector3 pos)
    {
        foreach(GameObject grunt in grunts)
        {
            if (!grunt.activeInHierarchy)
            {
                grunt.transform.position = pos;
                grunt.SetActive(true);
                grunt.transform.SetParent(this.gameObject.transform);
                return grunt;
            }
        }
        GameObject newGrunt = Instantiate(gruntPerfabs,pos,Quaternion.identity);
        grunts.Add(newGrunt);
        newGrunt.transform.SetParent(this.gameObject.transform);
        return newGrunt;
    }
    public void ReturnGruntEnemy(GameObject grunt)
    {
        grunt.SetActive(false);
    }
}
