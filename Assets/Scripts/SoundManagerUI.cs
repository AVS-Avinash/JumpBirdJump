using UnityEngine;

public class SoundManagerUI : MonoBehaviour {

    public static SoundManagerUI Instance { get; private set; }

    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip buttonOver;

    private AudioSource audioSource;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
            audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayButtonSelectSound() {
        audioSource.PlayOneShot(buttonOver, 1f);
    }

    public void PlayButtonClickSound() {
        audioSource.PlayOneShot(buttonClick, 1f);
    }


}
