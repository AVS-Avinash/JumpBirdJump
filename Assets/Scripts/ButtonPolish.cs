using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPolish : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler {


    [SerializeField] private float verticalMoveAmount = 15f;
    [SerializeField] private float moveTime = .1f;
    [Range(0f, 2f), SerializeField] private float scaleAmount = 1.2f;

    private Vector3 startPosition;
    private Vector3 startScale;

    private void Start() {
        startPosition = transform.localPosition;
        startScale = transform.localScale;
    }

    private IEnumerator MoveButton(bool startAnimation) {
        Vector3 endPosition;
        Vector3 endScale;

        if (startAnimation) {
            endPosition = startPosition + new Vector3(0, verticalMoveAmount, 0);
            endScale = startScale * scaleAmount;
        }
        else {
            endPosition = startPosition;
            endScale = startScale;
        }

        float elapsedTime = 0f;
        while (elapsedTime < moveTime) {

            elapsedTime += Time.unscaledDeltaTime;

            float lerpTime = elapsedTime / moveTime;

            Vector3 lerpedPosition = Vector3.Lerp(transform.localPosition, endPosition, lerpTime);
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, lerpTime);

            transform.localPosition = lerpedPosition;
            transform.localScale = lerpedScale;

            yield return null;
        }
    }



    public void OnPointerEnter(PointerEventData eventData) {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData) {
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData) {
        StartCoroutine(MoveButton(true));
        if (SoundManagerUI.Instance != null) {
            SoundManagerUI.Instance.PlayButtonSelectSound();
        }


        ButtonSelectionManager.Instance.lastSelected = gameObject;
        for (int i = 0; i < ButtonSelectionManager.Instance.buttonList.Length; i++) {
            if (ButtonSelectionManager.Instance.buttonList[i] == gameObject) {
                ButtonSelectionManager.Instance.lastSelectedIndex = i;
                return;
            }
        }
    }

    public void OnDeselect(BaseEventData eventData) {
        StartCoroutine(MoveButton(false));
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (SoundManagerUI.Instance != null) {
            SoundManagerUI.Instance.PlayButtonClickSound();
        }
    }
}
