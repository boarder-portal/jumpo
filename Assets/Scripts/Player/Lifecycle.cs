using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player {
  public class Lifecycle : MonoBehaviour {
    public bool IsAlive { get; private set; } = true;

    private void OnTriggerEnter2D(Collider2D collision) {
      if (IsAlive && collision.gameObject.CompareTag("Spikes")) {
        Die();
      }
    }

    public void Die() {
      if (!IsAlive) {
        return;
      }

      IsAlive = false;

      Invoke(nameof(RestartLevel), 1.5f);
    }

    private void RestartLevel() {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }
}
