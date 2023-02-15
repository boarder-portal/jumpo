using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Core {
  public class CoreLoader : MonoBehaviour {
    private void Start() {
      var coreScene = UnitySceneManager.GetSceneByName("Core");

      if (!coreScene.isLoaded) {
        UnitySceneManager.LoadScene(SceneManager.GetSceneName(SceneManager.Scene.Core), LoadSceneMode.Additive);
      }
    }
  }
}
