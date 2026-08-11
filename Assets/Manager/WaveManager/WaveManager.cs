using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Pool;

/// <summary>
///  WaveManager is used to track the progression of the level
/// </summary>
public class WaveManager : MonoBehaviour
{
    public event Action<int> WaveStarted;
    public event Action<int> WaveEnded;
    public event Action<int> WaveCountdowned;

    [SerializeField] private List<WaveData> waveDatas;
    private WaveData currentWave;
    private int currentWaveIndex = -1;
    private float currentSpawnCountdown = 0.0f;
    private float currentWaveCountdown = 0.0f;

    [SerializeField] public EntityManager entityManager;

    private void SpawnIntervalCountdown(float delta)
    {
        currentSpawnCountdown -= delta;

        if (currentSpawnCountdown <= 0.0f)
        {
            AttemptSpawnEnemy();
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

        entityManager.SetNewEnemyPool(currentWave.enemyPool);

        Debug.Log("Start wave " + currentWaveIndex);
        WaveStarted?.Invoke(currentWaveIndex);
    }

    private void EndCurrentWave()
    {
        Debug.Log("End wave " + currentWaveIndex);
        WaveEnded?.Invoke(currentWaveIndex);
    }

    private void AttemptSpawnEnemy()
    {
        entityManager.SpawnEnemy();
    }

    public void StartGame()
    {
        StartNewWave();
    }

    public void ProgressWave()
    {
        SpawnIntervalCountdown(Time.deltaTime);
        WaveCountdown(Time.deltaTime);
    }
}
