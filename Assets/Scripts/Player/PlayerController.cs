using UnityEngine;

namespace Player {
  using AnimationState = Player.PlayerAnimationController.AnimationState;

  public class PlayerController : MonoBehaviour {
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float sideForce = 7f;
    [SerializeField] private float jumpFallThreshold = 0.001f;

    private Rigidbody2D _rigidbody;
    private PlayerAnimationController _animationController;
    private SpriteRenderer _sprite;

    private void Start() {
      _rigidbody = GetComponent<Rigidbody2D>();
      _animationController = GetComponent<PlayerAnimationController>();
      _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
      var currentVelocity = _rigidbody.velocity;
      var newVelocityX = currentVelocity.x;
      var newVelocityY = currentVelocity.y;
      var newAnimationState = AnimationState.Idle;

      var isLeft = Input.GetKey("a");
      var isRight = Input.GetKey("d");

      if (isLeft || isRight) {
        newAnimationState = AnimationState.Run;

        if (isLeft && !isRight) {
          newVelocityX = -sideForce;
          _sprite.flipX = true;
        } else if (!isLeft) {
          newVelocityX = sideForce;
          _sprite.flipX = false;
        }
      } else {
        newVelocityX = 0;
      }

      if (Input.GetKeyDown("space")) {
        newVelocityY = jumpForce;
      }

      if (newVelocityY > jumpFallThreshold) {
        newAnimationState = AnimationState.Jump;
      } else if (newVelocityY < -jumpFallThreshold) {
        newAnimationState = AnimationState.Fall;
      }

      _rigidbody.velocity = new Vector2(newVelocityX, newVelocityY);

      _animationController.SetState(newAnimationState);
    }
  }
}
