using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public struct EnemySpawnEntry
{
    public Enemy prefab;

    // The chance this enemy got spawned during a spawn attempt
    public float spawnChance;
}

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{   
    [SerializeField] public int waveIndex;
    [SerializeField] public float waveDuration;
    [SerializeField] public float spawnInterval;
    [SerializeField] public List<EnemySpawnEntry> enemyPool;
}
