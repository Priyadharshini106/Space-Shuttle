using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawning : MonoBehaviour
{
    public static BulletSpawning bulletSpawning;
    GameObject bulletPrefab;
    public int poolsize = 15;
    public List<GameObject> BulletPool;
    public float bulletSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        bulletSpawning = this;
        BulletPool = new List<GameObject>();
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        for (int i = 0; i < poolsize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            BulletPool.Add(bullet);
            bullet.SetActive(false);
        }
    }

    public void OnShoot()
    {
        GameObject bullet = GetBulletFromPool();
        if (bullet != null)
        {
            bullet.transform.position = this.transform.position;
            bullet.SetActive(true);
        }
    }


    public GameObject GetBulletFromPool()
    {
        foreach (GameObject bullet in BulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        return null;
    }

    public void Disablebullet()
    {
        foreach (GameObject bullet in BulletPool)
            bullet.SetActive(false);
    }
}
