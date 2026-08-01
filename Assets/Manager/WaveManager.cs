using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

/// <summary>
///  WaveManager is used to track the progression of the level
/// </summary>
public class WaveManager : MonoBehaviour
{
    private const float MINIMUM_SPAWN_DISTANCE = 15.0F;
    private const float MAXIMUM_SPAWN_DISTANCE = 25.0F;
    public event Action<int> WaveStarted;
    public event Action<int> WaveEnded;
    public event Action<int> WaveCountdowned;
    [SerializeField] private List<WaveData> waveDatas;
    private WaveData currentWave;
    private int currentWaveIndex = 0;
    private int currentMonsterWeight = 0;
    private float currentSpawnCountdown = 0.0f;
    private float currentWaveCountdown = 0.0f;

    private List<Enemy> enemyList;

    private void Awake()
    {
        StartNewWave();
    }

    private void SpawnIntervalCountdown(float delta)
    {
        currentSpawnCountdown -= delta;

        if (currentSpawnCountdown <= 0.0f)
        {
            AttemptSpawn();
            currentSpawnCountdown = currentWave.spawnInterval;
        }
    }

    private void WaveCountdown(float delta)
    {
        currentWaveCountdown -= delta;

        if (currentWaveCountdown <= 0.0f)
        {
            EndCurrentWave();
            StartNewWave();
        }
    }

    private void StartNewWave()
    {
        if ((currentWaveIndex + 1) != waveDatas.Count) {
            currentWaveIndex += 1;
        }

        currentWave = waveDatas[currentWaveIndex];

        currentSpawnCountdown = currentWave.spawnInterval;
        currentWaveCountdown = currentWave.waveDuration;

        UnityEngine.Debug.Log("Start wave " + currentWaveIndex);
        WaveStarted?.Invoke(currentWaveIndex);
    }

    private void EndCurrentWave()
    {
        UnityEngine.Debug.Log("End wave " + currentWaveIndex);
        WaveEnded?.Invoke(currentWaveIndex);
    }

    private void AttemptSpawn()
    {
        if (currentMonsterWeight >= currentWave.monsterWeightMax)
        {
            return;
        }

        WeightedEnemy monster = PickMonster();
        Vector3 spawnPosition = PickSpawnLocation();

        // Ask the Spawner to spawn
        Instantiate(monster.prefab, spawnPosition, Quaternion.identity);
        currentMonsterWeight += monster.spawnWeight;
        
        UnityEngine.Debug.Log("SPAWNING... " + monster.prefab.name);
    }

    private WeightedEnemy PickMonster()
    {
        int random = UnityEngine.Random.Range(0, 100);
        float accumulation = 0;

        foreach (WeightedEnemy enemy in currentWave.enemyPool)
        {
            if (random < (accumulation + enemy.spawnChance))
            {
                return enemy;
            }

            accumulation += enemy.spawnChance;
        }

        return currentWave.enemyPool[0];
    }

    private Vector3 PickSpawnLocation()
    {
        float randomDistance = UnityEngine.Random.Range(MINIMUM_SPAWN_DISTANCE, MAXIMUM_SPAWN_DISTANCE);
        float randomAngle = UnityEngine.Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;

        Vector3 spawnDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0);
        Vector3 spawnPosition = PlayerManager.Instance.currentPlayer.transform.position + (spawnDirection * randomDistance);

        return spawnPosition;
    }

    private void Update()
    {
        SpawnIntervalCountdown(Time.deltaTime);
        WaveCountdown(Time.deltaTime);
    }
}
