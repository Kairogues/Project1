using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
