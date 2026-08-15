using System;
using UnityEngine;

/// <summary>
///  PlayerManager is used to track everything about the player such as the player's alive state, position,...
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public event Action PlayerDied;

    public Player currentPlayer { get; private set; }
    public LifeComponent currentPlayerLifeComponent { get; private set; }

    [SerializeField] private PlayerXPManager playerXPManger;
    [SerializeField] private PlayerGoldManager playerGoldManager;

    public void RegisterPlayer(Player player, LifeComponent playerLifeComponent)
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

        currentPlayerLifeComponent = playerLifeComponent;
        currentPlayerLifeComponent.Died += AnnouncePlayerDeath;
    }

    private void AnnouncePlayerDeath()
    {
        PlayerDied?.Invoke();
    }
}
