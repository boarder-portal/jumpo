using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

namespace Shared.Utilities {
  public static class Scene {
    public static IEnumerable<UnityEngine.SceneManagement.Scene> GetAllScenes() {
      for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
        yield return SceneManager.GetSceneByBuildIndex(i);
      }
    }

    public static IEnumerable<string> GetAllSceneNames() {
      for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
        yield return Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
      }
    }
  }
}
