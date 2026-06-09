using UnityEngine;
using UnityEngine.Assemblies;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private StatsComponent statsComponent;
    private Stats movementStat;
    private Vector2 currentDirection;
    private float currentSpeed = 0.0f;

    private void Awake()
    {
        movementStat = statsComponent.GetStats(StatType.MOVEMENT_SPEED);
        SubscribeToMovementSpeedChanged(UpdateSpeedAfterChanged);

        UnityEngine.Debug.Log(gameObject.name + " has " + healthStat.GetCurrentValue() + " health");
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
        statsComponent.SubscribeToStat(StatType.MOVEMENT_SPEED, listener);
    }

    public void UpdateDirection(Vector2 newDirection)
    {
        currentDirection = newDirection;
    }
}
