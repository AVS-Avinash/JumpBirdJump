using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    public static SceneLoader Instance { get; private set; }

    [SerializeField] private Canvas canvas;

    private const string END_TRIGGER = "End";
    private const string START_TRIGGER = "Start";

    private Animator sceneTransitionAnimator;
    private readonly WaitForSeconds waitForHalfSecond = new(.5f);
    

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        sceneTransitionAnimator = GetComponent<Animator>();
    }

    private void Start() {
        StartCoroutine(DisableCanvasAfterEntry());
    }

    public enum Scene {
        GameScene,
        MainMenuScene,
    }

    public void LoadScene(Scene scene) {
        StartCoroutine(LoadNextScene(scene));
    }

    private IEnumerator LoadNextScene(Scene scene) {
        canvas.sortingOrder = 10;
        sceneTransitionAnimator.SetTrigger(END_TRIGGER);
        yield return waitForHalfSecond;
        SceneManager.LoadScene(scene.ToString());
        sceneTransitionAnimator.SetTrigger(START_TRIGGER);
        yield return waitForHalfSecond;
        canvas.sortingOrder = -10;
    }

    private IEnumerator DisableCanvasAfterEntry() {
        yield return waitForHalfSecond;
        canvas.sortingOrder = -10;
    }

}
