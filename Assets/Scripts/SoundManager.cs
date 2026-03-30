using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioClip birdJump;
    [SerializeField] private AudioClip lose;
    [SerializeField] private AudioClip score;

    private AudioSource audioSource;


    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        Bird.Instance.OnBirdJump += Bird_OnBirdJump;
        GameManager.Instance.OnScoreChange += GameManager_OnScoreChange;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void OnDestroy() {
        Bird.Instance.OnBirdJump -= Bird_OnBirdJump;
        GameManager.Instance.OnScoreChange -= GameManager_OnScoreChange;
        GameManager.Instance.OnGameOver -= GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver(object sender, bool e) {
        audioSource.PlayOneShot(lose, 1f);
    }

    private void GameManager_OnScoreChange(object sender, System.EventArgs e) {
        audioSource.PlayOneShot(score, 1f);
    }

    private void Bird_OnBirdJump(object sender, System.EventArgs e) {
        audioSource.PlayOneShot(birdJump, 1f);
    }
}
