using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance_;

    public Vector2 MoveInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool DashInput { get; private set; }

    public bool InventoryToggleInput { get; private set; }
    public bool MenuToggleInput{ get; private set; }

    private PlayerInput playerInput_;

    private InputAction moveAction_;
    private InputAction attackAction_;
    private InputAction jumpAction_;
    private InputAction dashAction_;
    private InputAction inventoryToggleAction_;
    private InputAction menuToggleAction_;

    private void Awake()
    {
        if(instance_ == null)
        {
            instance_ = this;
        }

        playerInput_ = GetComponent<PlayerInput>();
        SetUpInputActions();
    }

    private void Update()
    {
        UpdateInputActions();
    }

    private void SetUpInputActions()
    {
        moveAction_ = playerInput_.actions["Move"];
        attackAction_ = playerInput_.actions["Attack"];
        jumpAction_ = playerInput_.actions["Jump"];
        dashAction_ = playerInput_.actions["Dash"];
        menuToggleAction_ = playerInput_.actions["MenuToggle"];
    }

    private void UpdateInputActions()
    {
        MoveInput =              moveAction_.ReadValue<Vector2>();
        AttackInput =            attackAction_.WasPressedThisFrame();
        JumpInput =              jumpAction_.WasPressedThisFrame();
        DashInput =              dashAction_.WasPressedThisFrame();
        MenuToggleInput =        menuToggleAction_.WasPressedThisFrame();
    }
}
