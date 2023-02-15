using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Core {
  public class SceneManager : MonoBehaviour {
    public enum Scene {
      Core,
      MainMenu,
    }

    private static readonly Dictionary<Scene, string> SceneString = new() {
      { Scene.Core, "Core" },
      { Scene.MainMenu, "Main Menu" },
    };

    private static readonly Regex LevelRegex = new(@"^Level (\d+)$");

    private static IEnumerator UnloadAllScenesAsync(params string[] excludingScenes) {
      var operations = (
        from scene in Shared.Utilities.Scene.GetAllScenes()
        where
          scene.isLoaded
          && scene.name != GetSceneName(Scene.Core)
          && !excludingScenes.Contains(scene.name)
        select UnitySceneManager.UnloadSceneAsync(scene.name)
      ).ToList();

      while (operations.Any(operation => !operation.isDone)) {
        yield return null;
      }
    }

    private static IEnumerator LoadSceneAsync(string scene) {
      // TODO: figure out how to restart a scene without unloading it
      yield return UnloadAllScenesAsync();

      UnitySceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    }

    public static string GetSceneName(Scene scene) {
      return SceneString[scene];
    }

    public static int GetCurrentLevel() {
      foreach (var scene in Shared.Utilities.Scene.GetAllScenes()) {
        if (!scene.isLoaded) {
          continue;
        }

        var match = LevelRegex.Match(scene.name);

        if (match.Success) {
          return Convert.ToInt32(match.Groups[1].Value);
        }
      }

      return 0;
    }

    private int _levelsCount;

    private void Start() {
      _levelsCount = (
        from sceneName in Shared.Utilities.Scene.GetAllSceneNames()
        where LevelRegex.IsMatch(sceneName)
        select 1
      ).Count();

      LoadMainMenuIfNeeded();

      UnitySceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode) {
      LoadMainMenuIfNeeded();
    }

    private void LoadMainMenuIfNeeded() {
      if (UnitySceneManager.sceneCount == 1 && UnitySceneManager.GetActiveScene().name == GetSceneName(Scene.Core)) {
        LoadMainMenu();
      }
    }

    public void LoadScene(string scene) {
      StartCoroutine(LoadSceneAsync(scene));
    }

    public void LoadScene(Scene scene) {
      LoadScene(GetSceneName(scene));
    }

    public void LoadMainMenu() {
      LoadScene(Scene.MainMenu);
    }

    public void GoToLevel(int level) {
      LoadScene($"Level {level}");
    }

    public void GoToNextLevel() {
      var currentLevel = GetCurrentLevel();

      if (currentLevel == 0) {
        return;
      }

      if (currentLevel == _levelsCount) {
        LoadMainMenu();
      } else {
        GoToLevel(currentLevel + 1);
      }
    }

    public void RestartLevel() {
      var currentLevel = GetCurrentLevel();

      if (currentLevel != 0) {
        GoToLevel(currentLevel);
      }
    }
  }
}
