using System;
using Core;
using TMPro;
using UnityEngine;

namespace Player {
  public class AppleCollector : MonoBehaviour {
    public static event Action OnCollect;

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

        OnCollect?.Invoke();

        counterText.text = $"{levelManager.ApplesCount}";
      }
    }
  }
}
