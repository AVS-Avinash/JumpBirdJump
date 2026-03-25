using UnityEngine;

public class PipeHandler : MonoBehaviour {

    [SerializeField] private GameObject pipeSet;
    [SerializeField] private float pipeInitiateX;
    [SerializeField] private float pipeResetX;
    [SerializeField] private float pipeMaxY;
    [SerializeField] private float pipeMinY;

    private float pipeDistance = 40f;
    private int pipeCount = 6;
    private float pipeStartX;
    private Transform[] pipeTransformList;
    private bool shouldChange = false;
    private int pipesChanged = 0;

    private void Awake() {
        pipeTransformList = new Transform[pipeCount];
        pipeStartX = pipeInitiateX + (int)(2 * pipeDistance);
    }

    private void Start() {
        for (int i = 0; i < pipeCount; i++) {
            GameObject gameObject = Instantiate(pipeSet,
                new Vector3(pipeInitiateX, Random.Range(pipeMinY, pipeMaxY), 0),
                Quaternion.identity);

            pipeTransformList[i] = gameObject.transform;
            pipeInitiateX += pipeDistance;
        }
        GameManager.Instance.OnDifficultyChange += GameManager_OnDifficultyChange;
    }

    private void GameManager_OnDifficultyChange(object sender, GameManager.OnDifficultyChangeEventArgs e) {
        if (e.DifficultyLevel > 1) {
            shouldChange = true;
        }
    }

    private void Update() {
        foreach (Transform transform in pipeTransformList) {
            transform.position += GameManager.Instance.GetWorldMovingSpeed() *
            Time.deltaTime *
            Vector3.left;

            if (transform.position.x < pipeResetX) {
                float reduceGap = 2f;
                if (shouldChange) {
                    transform.GetChild(0).gameObject.transform.position += new Vector3(0, -reduceGap, 0);
                    transform.GetChild(1).gameObject.transform.position += new Vector3(0, reduceGap, 0);
                    pipesChanged++;
                }
                if (pipesChanged >= pipeCount) {
                    pipesChanged = 0;
                    shouldChange = false;
                    pipeMinY -= reduceGap;
                    pipeMaxY += reduceGap;
                }
                transform.position = new Vector3(pipeStartX,
                Random.Range(pipeMinY, pipeMaxY),
                transform.position.z);
            }
        }
    }

    private void OnDestroy() {
        GameManager.Instance.OnDifficultyChange -= GameManager_OnDifficultyChange;
    }


}
