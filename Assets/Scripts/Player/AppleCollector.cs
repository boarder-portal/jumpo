using UnityEngine;
using TMPro;

namespace Player {
  public class AppleCollector : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI counterText;

    private AudioManager _audioManager;

    private int _applesCount;

    private void Start() {
      _audioManager = GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Apple")) {
        _audioManager.Play(Audio.Collect);

        Destroy(collision.gameObject);

        _applesCount++;

        counterText.text = $"{_applesCount}";
      }
    }
  }
}
