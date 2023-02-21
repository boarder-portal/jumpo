using System;
using Player;
using UnityEngine;

namespace Core {
  public class LevelManager : MonoBehaviour {
    public static event Action<bool> OnPauseStateChanged;
    public static event Action OnCompleteLevel;

    [SerializeField] private GameObject applesContainer;

    private float _startTimestamp;
    private int _allApplesCount;

    public int ApplesCount { get; private set; }
    public bool IsCompleted { get; private set; }
    public bool IsPaused { get; private set; }

    private void Start() {
      _startTimestamp = Time.time;
      _allApplesCount = applesContainer.transform.childCount;
    }

    private void OnEnable() {
      AppleCollector.OnCollect += CollectApple;
    }

    private void OnDisable() {
      AppleCollector.OnCollect -= CollectApple;
    }

    private void Update() {
      if (Input.GetKeyDown(KeyCode.R)) {
        RestartLevel();
      }

      if (Input.GetKeyDown(KeyCode.Escape)) {
        SetPauseState(!IsPaused);
      }
    }

    private void CollectApple() {
      ApplesCount++;
    }

    public void CompleteLevel() {
      if (IsCompleted) {
        return;
      }

      IsCompleted = true;

      if (ApplesCount == _allApplesCount) {
        var currentLevel = CoreAPI.SceneManager.GetCurrentLevel();
        var score = Time.time - _startTimestamp;

        CoreAPI.HighScoreManager.TrySetLevelScore(currentLevel, score);
      }

      Invoke(nameof(StartNextLevel), 1f);

      OnCompleteLevel?.Invoke();
    }

    public void RestartLevel() {
      CoreAPI.SceneManager.RestartLevel();
    }

    private void SetPauseState(bool paused) {
      IsPaused = paused;
      Time.timeScale = paused ? 0 : 1;

      OnPauseStateChanged?.Invoke(paused);
    }

    private void StartNextLevel() {
      var loaded = CoreAPI.SceneManager.GoToNextLevel();

      if (!loaded) {
        CoreAPI.SceneManager.LoadMainMenu();
      }
    }
  }
}
