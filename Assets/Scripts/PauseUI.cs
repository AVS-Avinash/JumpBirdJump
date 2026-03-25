using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.PauseUnpauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Instance.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void Start() {
        GameManager.Instance.OnGamePause += GameManager_OnGamePause;
        GameManager.Instance.OnGameUnpause += GameManager_OnGameUnpause;

        Hide();
    }

    private void OnDestroy() {
        GameManager.Instance.OnGamePause -= GameManager_OnGamePause;
        GameManager.Instance.OnGameUnpause -= GameManager_OnGameUnpause;
    }

    private void GameManager_OnGameUnpause(object sender, System.EventArgs e) {
        Hide();
    }

    private void GameManager_OnGamePause(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }


}
