using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnOpearte2Action;
    public event EventHandler OnOperateAction;
    public event EventHandler OnPauseAction;
    private GameControl gameControl;

    private void Awake()
    {
        Instance = this; 

        gameControl = new GameControl();
        gameControl.Player.Enable();

        gameControl.Player.Interact.performed += Interact_Performed;
        gameControl.Player.Operate.performed += Operate_performed;
        gameControl.Player.Operate2.performed += Operate2_performed;
        gameControl.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        gameControl.Player.Interact.performed -= Interact_Performed;
        gameControl.Player.Operate.performed -= Operate_performed;
        gameControl.Player.Pause.performed -= Pause_performed;

        gameControl.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Operate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnOperateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Operate2_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnOpearte2Action?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementDirectionNormalized()
    {
        Vector2 inputVector2 = gameControl.Player.Move.ReadValue<Vector2>();

        Vector3 direction = new Vector3(inputVector2.x, 0, inputVector2.y);

        direction = direction.normalized;

        return direction;
    }

}
