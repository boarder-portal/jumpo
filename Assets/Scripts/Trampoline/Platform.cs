using UnityEngine;
using Collision = Shared.Utilities.Collision;

namespace Trampoline {
  public class Platform : MonoBehaviour {
    private JumpableTrampoline _trampoline;

    private void Start() {
      _trampoline = GetComponentInParent<JumpableTrampoline>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
      if (
        collision.gameObject.CompareTag("Player")
        && Collision.GetCollisionDirection(collision) == Collision.Direction.Up
      ) {
        _trampoline.Activate(collision.gameObject);
      }
    }
  }
}
