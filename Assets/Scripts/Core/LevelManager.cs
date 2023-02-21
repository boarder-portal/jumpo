using UnityEngine;

namespace Core {
  public class LevelManager : MonoBehaviour {
    [SerializeField] private GameObject applesContainer;

    private float _startTimestamp;
    private int _allApplesCount;

    public int ApplesCount { get; private set; }
    public bool IsCompleted { get; private set; }

    private void Start() {
      _startTimestamp = Time.time;
      _allApplesCount = applesContainer.transform.childCount;
    }

    public void CollectApple() {
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
    }

    public void RestartLevel() {
      CoreAPI.SceneManager.RestartLevel();
    }

    private void StartNextLevel() {
      var loaded = CoreAPI.SceneManager.GoToNextLevel();

      if (!loaded) {
        CoreAPI.SceneManager.LoadMainMenu();
      }
    }
  }
}
