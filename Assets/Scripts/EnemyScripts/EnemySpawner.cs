using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject smallRoamer;

    [SerializeField] private float roamerInterval = 3.5f;

    public int EnemiesPerWave = 5;
    public int NumberOfWaves = 3;
    public Transform[] SpawnPoint;

    public int CurrentWave = 0;
    private int enemiesSpawned = 0;
    private float spawnTimer = 0f;

   
    void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (enemiesSpawned < EnemiesPerWave)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= roamerInterval)
            {
                StartCoroutine(spawnEnemy(roamerInterval, smallRoamer));
                spawnTimer = 0f;
            }
        }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5), Random.Range(-6f, 6f), 0), Quaternion.identity);
        enemiesSpawned++;

        if (enemiesSpawned >= EnemiesPerWave)
        {
            if (CurrentWave >= NumberOfWaves)
            {
                Debug.LogError("All waves completed");
            }
            else
            {
                StartCoroutine(spawnEnemy(roamerInterval, smallRoamer));
            }
        }
        
    }

    private void StartNextWave()
    {
        CurrentWave++;
        enemiesSpawned = 0;

        switch (CurrentWave)
        {
            case 1:
                EnemiesPerWave = 5;
                roamerInterval = 2f;
                break;
            case 2:
                EnemiesPerWave = 10;
                roamerInterval = 1.5f;
                break;
            case 3:
                EnemiesPerWave = 15;
                roamerInterval = 1f;
                break;
        }
    }
    
}
