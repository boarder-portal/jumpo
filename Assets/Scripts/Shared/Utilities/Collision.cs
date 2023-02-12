using UnityEngine;

namespace Shared.Utilities {
  public static class Collision {
    public enum Direction {
      None,
      Up,
      Down,
      Left,
      Right,
    }

    public static Direction GetCollisionDirection(Collision2D collision) {
      foreach (var contact in collision.contacts) {
        if (contact.normal == Vector2.down) {
          return Direction.Up;
        }

        if (contact.normal == Vector2.up) {
          return Direction.Down;
        }

        if (contact.normal == Vector2.left) {
          return Direction.Right;
        }

        if (contact.normal == Vector2.right) {
          return Direction.Left;
        }
      }

      return Direction.None;
    }
  }
}
