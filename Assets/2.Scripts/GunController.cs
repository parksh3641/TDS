using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float detectionRange = 10f;
    private float fireRate = 1.0f;
    private float spreadAngle = 10f;
    private int bulletPoolSize = 50;

    private Transform targetZombie;
    private float nextFireTime = 0f;
    private List<GameObject> bulletPool = new List<GameObject>();
    private int currentBulletIndex = 0;

    void Awake()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    void Update()
    {
        targetZombie = FindClosestZombie();

        if (targetZombie != null)
        {
            LookAtTarget(targetZombie);

            if (Time.time >= nextFireTime)
            {
                FireShotgun();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    Transform FindClosestZombie()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        Transform closestZombie = null;
        float minDistance = detectionRange;

        foreach (GameObject zombie in zombies)
        {
            float distance = Vector2.Distance(transform.position, zombie.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestZombie = zombie.transform;
            }
        }

        return closestZombie;
    }

    void LookAtTarget(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void FireShotgun()
    {
        for (int i = -1; i <= 1; i++)
        {
            GameObject bullet = GetNextBullet();
            if (bullet != null)
            {
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = firePoint.rotation * Quaternion.Euler(0, 0, i * spreadAngle);
                bullet.SetActive(true);
            }
        }
    }

    GameObject GetNextBullet()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = bulletPool[currentBulletIndex];
            currentBulletIndex = (currentBulletIndex + 1) % bulletPoolSize;
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        return null;
    }
}
