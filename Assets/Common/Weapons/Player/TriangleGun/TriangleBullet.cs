using System;
using UnityEngine;

public class TriangleBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private MovementComponent movementComponent;
    private float timeFly = 0.2f;
    private float spinTime = 3.0f;

    private void Awake()
    {
        movementComponent.SetBody(body);
        movementComponent.UpdateDirection(transform.right);
    }

    private void Update()
    {
        timeFly -= Time.deltaTime;
        if (timeFly <= 0.0f)
        {
            SpinInPlace();
            spinTime -= Time.deltaTime;
            if (spinTime <= 0.0f)
            {
                SelfDestruct();
            }
        }
    }

    private void SpinInPlace()
    {
        movementComponent.UpdateDirection(Vector2.zero);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
