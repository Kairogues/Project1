using System;
using UnityEngine;

public class OrbBullet : MonoBehaviour
{
    [SerializeField] private HitboxComponent hitboxComponent;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private MovementComponent movementComponent;
    private float timeAlive = 3.0f;



    private void Awake()
    {
        movementComponent.SetBody(body);
        movementComponent.UpdateDirection(transform.right);
    }


    private void Start()
    {
        hitboxComponent.HitHurtbox += ProcessHitHurtbox;
    }


    private void Update()
    {
        timeAlive -= Time.deltaTime;
        if (timeAlive <= 0.0f)
        {
            SelfDestruct();
        }
    }


    private void ProcessHitHurtbox(HurtboxComponent hurtboxComponent)
    {
        SelfDestruct();
    }


    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
