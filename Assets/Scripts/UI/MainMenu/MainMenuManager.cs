using Core;
using UnityEngine;

namespace UI.MainMenu {
  public class MainMenuManager : MonoBehaviour {
    public void StartGame() {
      CoreAPI.SceneManager.LoadScene(SceneManager.Scene.LevelsList);
    }

    public void QuitGame() {
      Application.Quit();
    }
  }
}
