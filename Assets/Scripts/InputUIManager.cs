using UnityEngine;
using UnityEngine.InputSystem;

public class InputUIManager : MonoBehaviour {

    public static InputUIManager Instance { get; private set; }

    public Vector2 navigationInput;

    private InputAction NavigationAction { get; set; }

    public static PlayerInput PlayerInput;

    private void Awake() {
        Instance = this;

        PlayerInput = GetComponent<PlayerInput>();
        NavigationAction = PlayerInput.actions["Navigate"];
    }

    private void Update() {
        navigationInput = NavigationAction.ReadValue<Vector2>();
    }
}