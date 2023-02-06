using UnityEngine;

namespace Player {
  public class PlayerController : MonoBehaviour {
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float sideForce = 10f;

    private Rigidbody2D _rigidbody;

    private void Start() {
      _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
      var newVelocityX = 0f;
      var newVelocityY = _rigidbody.velocity.y;

      if (Input.GetKey("a")) {
        newVelocityX = -sideForce;
      } else if (Input.GetKey("d")) {
        newVelocityX = sideForce;
      }

      if (Input.GetKeyDown("space")) {
        newVelocityY = jumpForce;
      }

      _rigidbody.velocity = new Vector2(newVelocityX, newVelocityY);
    }
  }
}
