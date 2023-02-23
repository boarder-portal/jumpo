using System;
using Player;
using UnityEngine;
using AppleLifecycle = Apple.Lifecycle;
using LevelUIManager = UI.Level.Manager;

namespace Core {
  public class LevelManager : MonoBehaviour {
    public static LevelManager Current;

    public static event Action<bool> OnPauseStateChanged;
    public static event Action OnCompleteLevel;

    private float _startTimestamp;
    private int _allApplesCount;
    private int _collectedApplesCount;

    public bool IsCompleted { get; private set; }
    public bool IsPaused { get; private set; }

    private void Awake() {
      Current = this;
    }

    private void Start() {
      _startTimestamp = Time.time;
    }

    private void OnEnable() {
      AppleLifecycle.OnSpawn += OnCreateApple;
      AppleCollector.OnCollect += OnCollectApple;
    }

    private void OnDisable() {
      AppleLifecycle.OnSpawn -= OnCreateApple;
      AppleCollector.OnCollect -= OnCollectApple;
    }

    private void OnDestroy() {
      Current = null;

      SetPauseState(false);
    }

    private void Update() {
      if (Input.GetKeyDown(KeyCode.R)) {
        RestartLevel();
      }

      if (Input.GetKeyDown(KeyCode.Escape)) {
        SetPauseState(!IsPaused);
      }
    }

    private void OnCreateApple() {
      _allApplesCount++;
    }

    private void OnCollectApple(int applesCount) {
      _collectedApplesCount = applesCount;
    }

    public void CompleteLevel() {
      if (IsCompleted) {
        return;
      }

      IsCompleted = true;

      if (_collectedApplesCount == _allApplesCount) {
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

    public void SetPauseState(bool paused) {
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
