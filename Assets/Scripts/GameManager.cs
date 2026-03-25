using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    private static int score;


    private const float INITIAL_WORLD_MOVING_SPEED = 15f;
    private const int MAX_DIFFICULTY_LEVEL = 3;
    private const float MAX_WORLD_MOVING_SPEED = 25f;
    private float worldMovingSpeed;
    private float elapsedTime;
    private int difficultyLevel;

    public event EventHandler OnScoreChange;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnpause;
    public event EventHandler<bool> OnGameOver;
    public event EventHandler<OnDifficultyChangeEventArgs> OnDifficultyChange;
    public class OnDifficultyChangeEventArgs : EventArgs {
        public int DifficultyLevel { get; private set; }
        public OnDifficultyChangeEventArgs(int level) {
            DifficultyLevel = level;
        }
    }

    private void Awake() {
        Instance = this;
        score = 0;
        difficultyLevel = 0;
    }

    private void Start() {
        Bird.Instance.OnStateChange += Bird_OnStateChange;
        Bird.Instance.OnSuccessfulPipePass += Bird_OnSuccessfulPipePass;

        GameInput.Instance.OnMenuAction += GameInput_OnMenuAction;
    }

    private void Update() {
        HandleDifficulty();
    }

    private void HandleDifficulty() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 5) {
            if (worldMovingSpeed < MAX_WORLD_MOVING_SPEED) {
                worldMovingSpeed++;
            }
            elapsedTime = 0f;
        }
    


        int calculatedLevel = Mathf.FloorToInt((worldMovingSpeed - INITIAL_WORLD_MOVING_SPEED) / 5f) + 1;
        if (calculatedLevel > difficultyLevel && difficultyLevel < MAX_DIFFICULTY_LEVEL) {
           difficultyLevel = calculatedLevel;
           OnDifficultyChange?.Invoke(this, new OnDifficultyChangeEventArgs(difficultyLevel));
        }
    }

    private void GameInput_OnMenuAction(object sender, EventArgs e) {
        PauseUnpauseGame();
    }

    private void Bird_OnSuccessfulPipePass(object sender, EventArgs e) {
        score++;
        OnScoreChange?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy() {
        Bird.Instance.OnStateChange -= Bird_OnStateChange;
        Bird.Instance.OnSuccessfulPipePass -= Bird_OnSuccessfulPipePass;
        GameInput.Instance.OnMenuAction -= GameInput_OnMenuAction;
    }

    private void Bird_OnStateChange(object sender, Bird.OnStateChangeEventArgs e) {
        switch (e.state) {
            default:

            case Bird.State.WaitingToStart:
                worldMovingSpeed = 0f;
                break;

            case Bird.State.Running:
                worldMovingSpeed = INITIAL_WORLD_MOVING_SPEED;
                break;

            case Bird.State.GameOver:
                worldMovingSpeed = 0f;
                OnGameOver?.Invoke(this, TrySetHighScore());
                
                break;

        }
    }

    private bool TrySetHighScore() {
        if (score > PlayerPrefs.GetInt("highscore", 0)) {
            PlayerPrefs.SetInt("highscore", score);
            return true;
        }
        return false;
    }

    public float GetWorldMovingSpeed() {
        return worldMovingSpeed;
    }

    public int GetScore() {
        return score;
    }

    public void PauseUnpauseGame() {
        if (Time.timeScale != 1f) {
            UnpauseGame();
        } else {
            PauseGame();
        }
    }

    private void UnpauseGame() {
        Time.timeScale = 1f;
        OnGameUnpause?.Invoke(this, EventArgs.Empty);
    }

    private void PauseGame() {
        Time.timeScale = 0f;
        OnGamePause?.Invoke(this, EventArgs.Empty);
    }


}
