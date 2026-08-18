using UnityEngine;

public class RedBoi : Enemy
{
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private LifeComponent lifeComponent;
    


    private void Start()
    {
        lifeComponent.Died += Die;
    }


    void Update()
    {
        
    }


    public override void OnSpawn()
    {
        base.OnSpawn();
    }


    public override void OnDespawn()
    {
        base.OnDespawn();
    }


    protected override void Die()
    {
        base.Die();
    }
}
