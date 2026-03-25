using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public static GameInput Instance { get; private set; }

    private InputActions inputActions;

    public event EventHandler OnMenuAction;

    private void Awake() {
        Instance = this;

        inputActions = new InputActions();
        inputActions.Enable();
    }

    private void Update() {
        inputActions.Bird.Menu.performed += Menu_performed;
    }

    private void Menu_performed(InputAction.CallbackContext obj) {
        OnMenuAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy() {
        inputActions.Bird.Menu.performed += Menu_performed;
        inputActions.Disable();
    }

    public bool IsJumpActionPerformed() {
        if (inputActions.Bird.Jump.WasPressedThisDynamicUpdate()) {
            return true;
        }
        return false;
    }

}
