using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING, FINISHED };

    [System.Serializable]
    public class Wave
    {
        public string Name;
        public Transform Enemy;
        public int BaseCount;
        public int CurrentCount;
        public float Rate;
    }

    public Wave[] Waves;
    private int nextWave = 0;

    public Transform[] SpawnPoints;

    public float TimeBetweenWaves = 5f;
    public float WaveCountdown;

    private int currentEnemyCount = 0;
    private float nextWaveEnemyMultiplier = 1.5f;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    public GameObject TriggerNext1;
    public GameObject TriggerNext2;
    public GameObject TriggerNext3;
    private int roomCount = 0;

    private void Start()
    {
        WaveCountdown = TimeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                return;
            }
        }

        if (state == SpawnState.FINISHED)
        {
            return;
        }

        if (WaveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(Waves[nextWave]));
            }
        }
        else
        {
            WaveCountdown -= Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        WaveCountdown = TimeBetweenWaves;

        if (nextWave + 1 > Waves.Length - 1)
        {
            state = SpawnState.FINISHED;
        }
        else
        {
            nextWave++;
        }

        currentEnemyCount = 0;

        if (roomCount == 0)
        {
            TriggerNext1.SetActive(true);
        }
        else if (roomCount == 1)
        {
            TriggerNext2.SetActive(true);
        }
        else if (roomCount == 1)
        {
            TriggerNext3.SetActive(true);
        }
    }


    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null && currentEnemyCount == 0)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        _wave.CurrentCount = _wave.BaseCount + (int)(currentEnemyCount * nextWaveEnemyMultiplier);

        for (int i = 0; i < _wave.CurrentCount; i++)
        {
            SpawnEnemy(_wave.Enemy);
            yield return new WaitForSeconds(4 / _wave.Rate);
        }

        state = SpawnState.WAITING;
        currentEnemyCount = _wave.CurrentCount;
        yield break;
    }


    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }


}
