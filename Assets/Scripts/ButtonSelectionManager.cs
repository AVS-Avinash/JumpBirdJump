using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectionManager : MonoBehaviour {

    public static ButtonSelectionManager Instance { get; set; }

    public GameObject[] buttonList;

    public GameObject lastSelected;
    public int lastSelectedIndex;

    private void Awake() {
        Instance = this;
    }

    private void OnEnable() {
        StartCoroutine(SetSelectedAfterOneFrame());
    }

    private void Update() {
        
        if (InputUIManager.Instance.navigationInput.y > 0) {
            HandleSelectNextButton(1);
        }

        if (InputUIManager.Instance.navigationInput.y < 0) {
            HandleSelectNextButton(-1);
        }
    }

    private IEnumerator SetSelectedAfterOneFrame() {
        yield return null;
        EventSystem.current.SetSelectedGameObject(buttonList[0]);
    }

    private void HandleSelectNextButton(int addition) {
        if (EventSystem.current.currentSelectedGameObject == null && lastSelected != null) {
            int newIndex = lastSelectedIndex + addition;
            newIndex = Mathf.Clamp(newIndex, 0, buttonList.Length - 1);
            EventSystem.current.SetSelectedGameObject(buttonList[newIndex]);
        }
        
    }

}
