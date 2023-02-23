using Core;
using Player;
using TMPro;
using UI.Shared;
using UnityEngine;

namespace UI.Level {
  public class Manager : MonoBehaviour {
    [SerializeField] private VisibilityToggler pauseMenuVisibilityToggler;
    [SerializeField] private TextMeshProUGUI applesCountText;

    private void Start() {
      pauseMenuVisibilityToggler.SetVisible(false);
    }

    private void OnEnable() {
      LevelManager.OnPauseStateChanged += OnPauseStateChanged;
      AppleCollector.OnCollect += OnCollectApple;
    }

    private void OnDisable() {
      LevelManager.OnPauseStateChanged -= OnPauseStateChanged;
      AppleCollector.OnCollect -= OnCollectApple;
    }

    private void OnCollectApple(int applesCount) {
      applesCountText.text = $"{applesCount}";
    }

    private void OnPauseStateChanged(bool paused) {
      pauseMenuVisibilityToggler.SetVisible(paused);
    }

    public void Unpause() {
      LevelManager.Current.SetPauseState(false);
    }

    public void GoToMainMenu() {
      CoreAPI.SceneManager.LoadMainMenu();
    }

    public void QuitGame() {
      CoreAPI.SceneManager.Quit();
    }
  }
}
