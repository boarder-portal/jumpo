using Core;
using UnityEngine;

namespace Music {
  public class BackgroundMusicManager : MonoBehaviour {
    private AudioSource _backgroundMusic;

    private void Awake() {
      _backgroundMusic = GetComponent<AudioSource>();
    }

    private void OnEnable() {
      LevelManager.OnPauseStateChanged += OnPauseStateChanged;
    }

    private void OnDisable() {
      LevelManager.OnPauseStateChanged -= OnPauseStateChanged;
    }

    private void OnPauseStateChanged(bool paused) {
      if (paused) {
        _backgroundMusic.Pause();
      } else {
        _backgroundMusic.Play();
      }
    }
  }
}
