using UnityEngine;

namespace Core {
  public class CoreAPI : MonoBehaviour {
    private static CoreAPI _instance;

    public static SceneManager SceneManager => _instance._sceneManager;
    public static UIAudioManager UIAudioManager => _instance._uiAudioManager;
    public static HighScoreManager HighScoreManager => _instance._highScoreManager;

    private SceneManager _sceneManager;
    private UIAudioManager _uiAudioManager;
    private HighScoreManager _highScoreManager;

    private void Awake() {
      if (_instance == null) {
        _instance = this;

        _sceneManager = GetComponent<SceneManager>();
        _uiAudioManager = GetComponent<UIAudioManager>();
        _highScoreManager = HighScoreManager.Load();

        DontDestroyOnLoad(gameObject);
      } else {
        Destroy(gameObject);
      }
    }
  }
}
