using UnityEngine;

namespace Core {
  public class CoreAPI : MonoBehaviour {
    private static CoreAPI _instance;

    public static SceneManager SceneManager => _instance._sceneManager;

    private SceneManager _sceneManager;

    private void Awake() {
      if (_instance == null) {
        _instance = this;

        _sceneManager = GetComponent<SceneManager>();

        DontDestroyOnLoad(gameObject);
      } else {
        Destroy(gameObject);
      }
    }
  }
}
