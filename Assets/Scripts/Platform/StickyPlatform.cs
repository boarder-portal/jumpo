using Player;
using UnityEngine;

namespace Platform {
  public class StickyPlatform : MonoBehaviour {
    [SerializeField] private Controller playerController;

    private void OnCollisionEnter2D(Collision2D collision) {
      if (collision.gameObject.CompareTag("Player")) {
        playerController.StickTo(transform);
      }
    }

    private void OnCollisionExit2D(Collision2D collision) {
      if (collision.gameObject.CompareTag("Player")) {
        playerController.Unstick();
      }
    }
  }
}
