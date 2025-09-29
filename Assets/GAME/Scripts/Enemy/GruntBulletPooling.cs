using System.Collections.Generic;
using UnityEngine;

public class GruntBulletPooling : MonoBehaviour
{
    [SerializeField] private GameObject bulletGruntPerfab;
    private List<GameObject> bulletGruntPool = new List<GameObject>();

    public GameObject GetBulletGrunt(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject bullet in bulletGruntPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;
                bullet.SetActive(true);
                return bullet;
            }
        }
        GameObject newBullet = Instantiate(bulletGruntPerfab,position,rotation,this.transform);
        bulletGruntPool.Add(newBullet);    
        return newBullet;
    }
    public void ReturnBulletGrunt(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
