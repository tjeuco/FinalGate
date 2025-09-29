using UnityEngine;
using System.Collections.Generic;

public class PlayerBulletPooling : MonoBehaviour
{
    [SerializeField] GameObject bulletPlayerPerfab;
    private List<GameObject> pool= new List<GameObject>();
    public GameObject GetBulletPlayer(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject bullet in pool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;   
                bullet.SetActive(true);
                return bullet;
            }
        }
        GameObject newBullet = Instantiate(bulletPlayerPerfab,position,rotation,transform);
        pool.Add(newBullet);
        return newBullet;
    }
    public void ReturnBulletPlayer(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}
