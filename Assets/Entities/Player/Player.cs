using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private AttackComponent attackComponent;
    [SerializeField] private LifeComponent lifeComponent;
    private InputAction moveAction;
    


    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        movementComponent.SetBody(body);
    }


    private void Start()
    {
        GameManager.Instance.playerManager.RegisterPlayer(this, lifeComponent);
    }


    void Update()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>().normalized;
        movementComponent.UpdateDirection(moveDirection);
        attackComponent.UpdateFacingDirection(moveDirection);
        attackComponent.AutoAttackAll();
    }
}
