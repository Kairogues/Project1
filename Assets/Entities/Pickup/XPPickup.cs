using UnityEngine;

public class XPPickup : Pickupable
{
    public override void OnSpawn()
    {
        GameManager.Instance.entityManager.RegisterXPOrb(this);
    }


    public override void OnDespawn()
    {
        GameManager.Instance.entityManager.UnregisterXPOrb(this);
    }

    
    protected override void OnDrop()
    {
        base.OnDrop();
    }


    protected override void OnPickup()
    {
        base.OnPickup();
    }


    protected override void ProcessPickup()
    {
        base.ProcessPickup();
    }
}
