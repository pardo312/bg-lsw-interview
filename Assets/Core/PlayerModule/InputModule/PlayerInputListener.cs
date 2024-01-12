using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputListener : MonoBehaviour
{
    #region ----Fields----
    public static PlayerInputListener Singleton;
    private GameplayInput playerInput;

    public Action onActionButtonPressed;
    public Action onInventory;
    public Action<Vector2> onMove;
    #endregion ----Fields----

    #region ----Methods----
    public void Awake()
    {
        if (Singleton != null)
        {
            Destroy(this);
            return;
        }

        Singleton = this;
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        playerInput = new GameplayInput();
        InputSubscribe();
    }

    private void InputSubscribe()
    {
        playerInput.Player.Move.performed += Move;
        playerInput.Player.Move.canceled += Move;
        playerInput.Player.Move.Enable();

        playerInput.Player.Action.started += Action;
        playerInput.Player.Action.Enable();

        playerInput.Player.Inventory.started += Inventory;
        playerInput.Player.Inventory.Enable();
    }

    public void Action(CallbackContext ctx)
    {
        onActionButtonPressed?.Invoke();
    }

    public void Inventory(CallbackContext ctx)
    {
        onInventory?.Invoke();
    }

    public void Move(CallbackContext ctx)
    {
        onMove?.Invoke(ctx.ReadValue<Vector2>());
    }

    public void OnDestroy()
    {
        playerInput.Player.Move.performed -= Move;
    }
    #endregion ----Methods----
}
