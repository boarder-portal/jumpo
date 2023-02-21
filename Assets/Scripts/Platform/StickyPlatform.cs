using Player;
using UnityEngine;
using Collision = Shared.Utilities.Collision;

namespace Platform {
  public class StickyPlatform : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
      if (
        collision.gameObject.CompareTag("Player")
        && Collision.GetCollisionDirection(collision) == Collision.Direction.Up
      ) {
        var playerController = collision.gameObject.GetComponent<Controller>();

        if (playerController) {
          playerController.StickTo(transform);
        }
      }
    }

    private void OnCollisionExit2D(Collision2D collision) {
      if (collision.gameObject.CompareTag("Player")) {
        var playerController = collision.gameObject.GetComponent<Controller>();

        if (playerController) {
          playerController.Unstick(transform);
        }
      }
    }
  }
}
