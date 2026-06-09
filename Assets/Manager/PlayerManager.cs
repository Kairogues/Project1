using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public event Action PlayerDied;

    public static PlayerManager Instance { get; private set; }
    public Player currentPlayer { get; private set; }

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

    public void RegisterPlayer(Player player)
    {
        if (currentPlayer == null)
        {
            currentPlayer = player;
            Debug.Log("Player successfully registered to PlayerManager.");
        }
        else if (currentPlayer != player)
        {
            Debug.LogWarning("A player is already registered! Overwriting reference.");
            currentPlayer = player;
        }

        LifeComponent lifecomp = currentPlayer.GetLifeComponent();
        lifecomp.Died += AnnouncePlayerDeath;
    }

    private void AnnouncePlayerDeath()
    {
        Debug.Log("Player died!");
        PlayerDied?.Invoke();
    }
}
