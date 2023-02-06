using UnityEngine;

namespace Player {
  public class PlayerController : MonoBehaviour {
    [SerializeField] private float jumpForce = 15f;

    private Rigidbody2D _rigidbody;

    void Start() {
      _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
      if (Input.GetKeyDown("space")) {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
      }
    }
  }
}