using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public GameObject zombiePrefab;
    public int poolSize = 50;
    public float spawnInterval = 1.0f;

    private List<GameObject> zombiePool = new List<GameObject>();
    private int currentZombieIndex = 0;
    private Vector3 spawnPosition = new Vector3(6.0f, -3.22f, 0f);

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject zombie = Instantiate(zombiePrefab);
            zombie.SetActive(false);
            zombiePool.Add(zombie);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        GameObject zombie = zombiePool[currentZombieIndex];
        zombie.SetActive(true);

        float randomZ = 0f;
        int layer = 0;

        switch (currentZombieIndex % 3)
        {
            case 0:
                randomZ = 0f;
                layer = LayerMask.NameToLayer("ZLayer-1");
                break;
            case 1:
                randomZ = 1.5f;
                layer = LayerMask.NameToLayer("ZLayer0");
                break;
            case 2:
                randomZ = 3f;
                layer = LayerMask.NameToLayer("ZLayer1");
                break;
        }

        zombie.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, randomZ);
        zombie.layer = layer;

        BoxCollider2D collider = zombie.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.gameObject.layer = layer;
        }

        currentZombieIndex = (currentZombieIndex + 1) % poolSize;
    }
}
