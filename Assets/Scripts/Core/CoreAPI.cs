using UnityEngine;

namespace Core {
  public class CoreAPI : MonoBehaviour {
    public static SceneManager SceneManager { get; private set; }

    private void Awake() {
      SceneManager = GetComponent<SceneManager>();
    }
  }
}
