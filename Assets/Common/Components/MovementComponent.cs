using System;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private StatComponent statComponent;
    private Stat movementStat;
    private Vector2 currentDirection;
    private float currentSpeed = 0.0f;

    private void Start()
    {
        movementStat = statComponent.GetStat(StatType.MOVEMENT_SPEED);
        currentSpeed = movementStat.GetCurrentValue();
        SubscribeToMovementSpeedChanged(UpdateSpeedAfterChanged);
    }

    private void FixedUpdate()
    {
        body.linearVelocity = currentDirection * currentSpeed * Time.fixedDeltaTime;
    }

    private void UpdateSpeedAfterChanged(float oldValue, float currentValue, float maxValue)
    {
        SetSpeed(currentValue);
    }

    public void SetSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }

    public void SetBody(Rigidbody2D body)
    {
        this.body = body;
    }

    private void SubscribeToMovementSpeedChanged(Action<float, float, float> listener) 
    {
        statComponent.SubscribeToStat(StatType.MOVEMENT_SPEED, listener);
    }

    public void UpdateDirection(Vector2 newDirection)
    {
        currentDirection = newDirection;
    }
}
