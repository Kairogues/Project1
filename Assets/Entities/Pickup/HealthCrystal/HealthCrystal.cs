using UnityEngine;

public class HealthCrystal : Pickupable
{
    [SerializeField] private int amount;


    public override void OnSpawn()
    {
        base.OnSpawn();
    }


    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    
    public override void OnDrop()
    {
        base.OnDrop();
    }


    public override void OnPickup()
    {
        // Turn off hitbox
        // Play pickup animation
        // Play SFX
    }


    public override void ProcessPickup(PickUpItemComponent actor)
    {
        OnPickup();

        GameManager.Instance.playerManager.currentPlayerLifeComponent.Heal(amount);

        ReleaseToPool();
    }
}
