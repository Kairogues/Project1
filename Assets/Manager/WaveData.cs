using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public struct WeightedEnemy
{
    public GameObject prefab;

    // The "space" this enemy takes up
    public int spawnWeight;

    // The chance this enemy got spawned during a spawn attempt
    public float spawnChance;
}

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{   
    [SerializeField] public int waveIndex;
    [SerializeField] public float waveDuration;
    [SerializeField] public int monsterWeightMax = 50;
    [SerializeField] public float spawnInterval;
    [SerializeField] public List<WeightedEnemy> enemyPool;
}
