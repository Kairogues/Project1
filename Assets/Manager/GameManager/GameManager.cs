using UnityEngine;
using System;
using System.Collections;

/// <summary>
///  GameManager is a singleton used to track every other managers, it controls the current level and state of the game
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public PlayerManager playerManager;
    [SerializeField] public WaveManager waveManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        playerManager.PlayerDied += OnPlayerDeath;
    }

    private void OnDisable()
    {
        playerManager.PlayerDied -= OnPlayerDeath;
    }

    private void ProgressWave()
    {
        waveManager.ProgressWave();
    }
    
    void Start()
    {
        waveManager.StartGame();
    }

    private void Update()
    {
        ProgressWave();
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Player died!");
    }


    // Handle player death

    // Handle wave progression

    // Handle monster spawning

}