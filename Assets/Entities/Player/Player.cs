using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private AttackComponent attackComponent;
    [SerializeField] private InputActionReference moveInput;

    private InputActionAsset inputActions;

    private InputAction moveAction;

    void OnEnalble()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    // void OnDisable()
    // {
    //     inputActions.FindActionMap("Player").Disable();
    // }
    
    void Awake()
    {
        
        moveAction = InputSystem.actions.FindAction("Move");
        movementComponent.SetBody(body);
    }

    private void Start()
    {
        PlayerManager.Instance.RegisterPlayer(this);
    }

    void Update()
    {
        // movementComponent.UpdateDirection(moveInput.action.ReadValue<Vector2>().normalized);
        movementComponent.UpdateDirection(moveAction.ReadValue<Vector2>().normalized);
        attackComponent.AutoAttackAll();
    }
}
