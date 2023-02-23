using System;
using UnityEngine;
using LevelUIManager = UI.Level.Manager;

namespace Player {
  public class AppleCollector : MonoBehaviour {
    public static event Action<int> OnCollect;

    private AudioManager _audioManager;

    private int _collectedApplesCount;

    private void Awake() {
      _audioManager = GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Apple")) {
        _collectedApplesCount++;

        _audioManager.Play(Audio.Collect);

        Destroy(collision.gameObject);

        OnCollect?.Invoke(_collectedApplesCount);
      }
    }
  }
}
