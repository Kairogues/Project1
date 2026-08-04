using UnityEngine;
using System;
using System.Collections;

/// <summary>
///  GameManager is a singleton used to track every other managers, it controls the current level and state of the game
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerManager playerManager { get; private set; }
    public WaveManager waveManager { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);

        playerManager = gameObject.GetComponentInChildren<PlayerManager>();
        waveManager = gameObject.GetComponentInChildren<WaveManager>();
    }

    private void OnEnable()
    {
        playerManager.PlayerDied += OnPlayerDeath;
    }

    private void OnDisable()
    {
        playerManager.PlayerDied -= OnPlayerDeath;
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Player died!");
    }


    // Handle player death

    // Handle wave progression

    // Handle monster spawning

}