using System.Collections;
using UnityEngine;

public class CloudHandler : MonoBehaviour {


    [SerializeField] private GameObject clouds_1;
    [SerializeField] private GameObject clouds_2;
    [SerializeField] private GameObject clouds_3;
    [SerializeField] private float cloudInitiateX = -180;
    [SerializeField] private int cloudWidth = 120;
    [SerializeField] private int cloudCount = 4;

    private Transform[] cloudTransformList;
    private float cloudMovingSpeed;
    private float cloudStartX;
    private float cloudResetX;
    private WaitForSeconds waitForOneSecond = new(1f);

    private void Awake() {
        cloudTransformList = new Transform[cloudCount];
        cloudStartX = cloudInitiateX + (4 * cloudWidth);
        cloudResetX = cloudInitiateX;
    }

    private void Start() {
        for (int i = 0; i < cloudCount; i++) {
            GameObject gameObject = Instantiate(GetRandomCloud(),
                new Vector3(cloudInitiateX, 35f, 0),
                Quaternion.identity);
            cloudTransformList[i] = gameObject.transform;
            cloudInitiateX += cloudWidth;
        }
        StartCoroutine(SetCloudMovingSpeedInEverySecond());
    }

    private IEnumerator SetCloudMovingSpeedInEverySecond() {
        float parallexEffect = .7f;
        while (true) {
            cloudMovingSpeed = GameManager.Instance.GetWorldMovingSpeed() * parallexEffect;
            yield return waitForOneSecond;
        }
    }

    private void Update() {
        for (int i = 0; i < cloudTransformList.Length; i++) {
            Transform currentTransform = cloudTransformList[i];
            currentTransform.position += cloudMovingSpeed *
            Time.deltaTime *
            Vector3.left;

            if (currentTransform.position.x < cloudResetX) {
                Destroy(currentTransform.gameObject);

                GameObject gameObject = Instantiate(GetRandomCloud(),
                new Vector3(cloudStartX, 35f, 0),
                Quaternion.identity);

                cloudTransformList[i] = gameObject.transform;
            }
        }
    }

    private GameObject GetRandomCloud() {
        int random = Random.Range(1, 4);
        return random switch {
            1 => clouds_1,
            2 => clouds_2,
            3 => clouds_3,
            _ => null,
        };
    }

}
