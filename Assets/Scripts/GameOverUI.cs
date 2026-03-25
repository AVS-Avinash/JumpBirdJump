using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private TextMeshProUGUI isHighScoreTextMesh;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;


    private void Awake() {
        retryButton.onClick.AddListener(() => {
            SceneLoader.Instance.LoadScene(SceneLoader.Scene.GameScene);
        });
        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Instance.LoadScene(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void GameManager_OnGameOver(object sender, bool isHighScore) {
        Show(isHighScore);
    }

    private void Start() {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        Hide();
    }


    private void OnDestroy() {
        GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
    }

    private void Show(bool isHighScore) {
        gameObject.SetActive(true);

        
        if (isHighScore) {
            isHighScoreTextMesh.text = "NEW HIGHSCORE";
        } else {
            isHighScoreTextMesh.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore");
        }

        scoreTextMesh.text = "SCORE: " + GameManager.Instance.GetScore();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
