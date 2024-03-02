using System;
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
      LevelsList,
    }

    private static readonly Dictionary<Scene, string> SceneString = new() {
      { Scene.Core, "Core" },
      { Scene.MainMenu, "Main Menu" },
      { Scene.LevelsList, "Levels List" },
    };

    private static readonly Regex LevelRegex = new(@"^Level (\d+)$");

    public static string GetSceneName(Scene scene) {
      return SceneString[scene];
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
      UnitySceneManager.LoadSceneAsync(scene);
    }

    public void LoadScene(Scene scene) {
      LoadScene(GetSceneName(scene));
    }

    public void LoadMainMenu() {
      LoadScene(Scene.MainMenu);
    }

    public void Quit() {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.ExitPlaymode();
#else
      Application.Quit();
#endif
    }

    public int GetCurrentLevel() {
      return (
        from scene in Shared.Utilities.Scene.GetAllScenes()
        where scene.isLoaded
        select LevelRegex.Match(scene.name)
        into match
        where match.Success
        select Convert.ToInt32(match.Groups[1].Value)
      ).FirstOrDefault();
    }

    public void GoToLevel(int level) {
      LoadScene($"Level {level}");
    }

    public bool GoToNextLevel() {
      var currentLevel = GetCurrentLevel();

      if (currentLevel == 0 || currentLevel >= _levelsCount) {
        return false;
      }

      GoToLevel(currentLevel + 1);

      return true;
    }

    public void RestartLevel() {
      var currentLevel = GetCurrentLevel();

      if (currentLevel != 0) {
        GoToLevel(currentLevel);
      }
    }

    public int GetLevelCount() {
      return _levelsCount;
    }
  }
}
