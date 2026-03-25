using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private TextMeshProUGUI highScoreTextMesh;
    [SerializeField] private Button pauseMenuButton;

    private void Awake() {
        pauseMenuButton.onClick.AddListener(() => {
            GameManager.Instance.PauseUnpauseGame();
        });
    }

    private void Start() {
        GameManager.Instance.OnScoreChange += GameManager_OnScoreChange;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        highScoreTextMesh.text = "Highscore: " + PlayerPrefs.GetInt("highscore", 0);
    }

    private void GameManager_OnGameOver(object sender, bool e) {
        Destroy(gameObject);
    }

    private void OnDestroy() {
        GameManager.Instance.OnScoreChange -= GameManager_OnScoreChange;
        GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
    }

    private void GameManager_OnScoreChange(object sender, System.EventArgs e) {
        scoreTextMesh.text = GameManager.Instance.GetScore().ToString();
    }


}
