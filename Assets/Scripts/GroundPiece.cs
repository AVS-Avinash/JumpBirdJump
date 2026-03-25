using UnityEngine;

public class GroundPiece : MonoBehaviour {

    [SerializeField] private float groundStartX;
    [SerializeField] private float groundResetX;

    private void Update() {
        transform.position += GameManager.Instance.GetWorldMovingSpeed() *
            Time.deltaTime *
            Vector3.left;

        if (transform.position.x < groundResetX) {
            transform.position = new Vector3(groundStartX,
                transform.position.y,
                transform.position.z);
        }
    }


}
