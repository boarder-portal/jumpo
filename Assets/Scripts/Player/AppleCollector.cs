using Core;
using UnityEngine;
using TMPro;

namespace Player {
  public class AppleCollector : MonoBehaviour {
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private TextMeshProUGUI counterText;

    private AudioManager _audioManager;

    private void Start() {
      _audioManager = GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Apple")) {
        _audioManager.Play(Audio.Collect);

        Destroy(collision.gameObject);

        levelManager.CollectApple();

        counterText.text = $"{levelManager.ApplesCount}";
      }
    }
  }
}
