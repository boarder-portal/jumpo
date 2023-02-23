using Core;
using UnityEngine;

namespace UI.MainMenu {
  public class Manager : MonoBehaviour {
    public void StartGame() {
      CoreAPI.SceneManager.LoadScene(SceneManager.Scene.LevelsList);
    }

    public void QuitGame() {
      CoreAPI.SceneManager.Quit();
    }
  }
}
