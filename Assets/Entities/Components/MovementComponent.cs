using UnityEngine;
using UnityEngine.Assemblies;

public class MovementComponent : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 currentDirection;

    private float maxSpeed = 150.0f;
    private float currentSpeed;

    void Start()
    {
        currentSpeed = maxSpeed;
    }

    void FixedUpdate()
    {
        body.linearVelocity = currentDirection * currentSpeed * Time.fixedDeltaTime;
    }

    public void SetBody(Rigidbody2D body)
    {
        this.body = body;
    }

    public void UpdateDirection(Vector2 newDirection)
    {
        currentDirection = newDirection;
    }
}
