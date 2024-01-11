using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputListener : MonoBehaviour
{
    #region ----Fields----
    public PlayerMovementController playerMovementController;
    public PlayerAnimatorController playerAnimatorController;
    private GameplayInput playerInput;
    #endregion ----Fields----

    #region ----Methods----
    public void Start()
    {
        playerInput = new GameplayInput();
        playerInput.Player.Move.started += Move;
        playerInput.Player.Move.performed += Move;
        playerInput.Player.Move.canceled += Move;
        playerInput.Player.Move.Enable();
    }

    public void Move(CallbackContext ctx)
    {
        playerMovementController.SetDirection(ctx.ReadValue<Vector2>());
        playerAnimatorController.SetMovementAnimation(ctx.ReadValue<Vector2>());
    }

    public void OnDestroy()
    {
        playerInput.Player.Move.performed -= Move;
    }
    #endregion ----Methods----
}
