using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private MovementComponent movementComponent;
    [SerializeField] private AttackComponent attackComponent;
    [SerializeField] private LifeComponent lifeComponent;
    [SerializeField] private InputActionReference moveInput;

    private InputActionAsset inputActions;
    private InputAction moveAction;

    public LifeComponent GetLifeComponent()
    {
        return lifeComponent;
    }

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
        movementComponent.UpdateDirection(moveAction.ReadValue<Vector2>().normalized);
        attackComponent.AutoAttackAll();
    }
}
