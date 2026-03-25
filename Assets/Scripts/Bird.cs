using System;
using UnityEngine;

public class Bird : MonoBehaviour {

    public static Bird Instance { get; private set; }

    private const float NORMAL_GRAVITY_SCALE = 35f;

    private Rigidbody2D birdRigidbody2D;
    private State state;

    public event EventHandler OnSuccessfulPipePass;
    public event EventHandler OnBirdJump;
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public class OnStateChangeEventArgs : EventArgs {
        public State state;
    }

    public enum State {
        WaitingToStart,
        Running,
        GameOver,
    }

    private void Awake() {
        Instance = this;
        birdRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        SetState(State.WaitingToStart);
    }

    private void Update() {
        switch (state) {
            default:

            case State.WaitingToStart:
                if (GameInput.Instance.IsJumpActionPerformed()) {
                    Jump();
                    birdRigidbody2D.gravityScale = NORMAL_GRAVITY_SCALE;
                    SetState(State.Running);
                }
                break;

            case State.Running:
                if (Time.timeScale != 0f) {
                    if (GameInput.Instance.IsJumpActionPerformed()) {
                        Jump();
                    }
                    ReactToGravity();
                }
                break;

            case State.GameOver:
                birdRigidbody2D.linearVelocity = Vector2.zero;
                birdRigidbody2D.simulated = false;
                return;
        }
    }

    private void Jump() {
        float jumpVelocity = 100f;
        birdRigidbody2D.linearVelocityY = jumpVelocity;
        OnBirdJump?.Invoke(this, EventArgs.Empty);
    }

    private void ReactToGravity() {
        float controlRotation = .3f;
        transform.eulerAngles = new Vector3(0, 0, birdRigidbody2D.linearVelocityY * controlRotation);
    }

    private void OnCollisionEnter2D(Collision2D collider2D) {
        SetState(State.GameOver);
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        OnSuccessfulPipePass?.Invoke(this, EventArgs.Empty);
    }

    private void SetState(State state) {
        this.state = state;
        OnStateChange?.Invoke(this, new OnStateChangeEventArgs {
            state = state,
        });
    }

}
