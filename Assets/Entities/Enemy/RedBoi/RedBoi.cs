using UnityEngine;

public class RedBoi : Enemy
{
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private LifeComponent lifeComponent;
    
    private void Start()
    {
        lifeComponent.Died += Die;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Die()
    {
        base.Die();
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }
}
