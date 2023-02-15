using Core;
using UnityEngine;

namespace UI {
  public class MainMenuManager : MonoBehaviour {
    public void StartGame() {
      CoreAPI.SceneManager.GoToLevel(1);
    }

    public void QuitGame() {
      Application.Quit();
    }
  }
}
