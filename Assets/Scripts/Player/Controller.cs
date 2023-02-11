using System;
using UnityEngine;

namespace Player {
  public class Controller : MonoBehaviour {
    private static readonly KeyCode[] LeftKeys = { KeyCode.LeftArrow, KeyCode.A };
    private static readonly KeyCode[] RightKeys = { KeyCode.RightArrow, KeyCode.D };

    private static bool IsLeftPressed() {
      return Array.Exists(LeftKeys, Input.GetKey);
    }

    private static bool IsRightPressed() {
      return Array.Exists(RightKeys, Input.GetKey);
    }

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float sideForce = 7f;
    [SerializeField] private float jumpFallThreshold = 0.001f;

    private Rigidbody2D _rigidbody;
    private AnimationManager _animationManager;
    private SpriteRenderer _sprite;
    private Collider2D _collider;

    private void Start() {
      _rigidbody = GetComponent<Rigidbody2D>();
      _animationManager = GetComponent<AnimationManager>();
      _sprite = GetComponent<SpriteRenderer>();
      _collider = GetComponent<Collider2D>();
    }

    private void Update() {
      var currentVelocity = _rigidbody.velocity;
      var newVelocityX = 0f;
      var newVelocityY = currentVelocity.y;
      var newAnimationState = AnimationState.Idle;

      var isLeft = IsLeftPressed();
      var isRight = IsRightPressed();

      if (isLeft || isRight) {
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

      if (Input.GetKeyDown("space") && IsGrounded()) {
        newVelocityY = jumpForce;
      }

      if (newVelocityX != 0) {
        newAnimationState = AnimationState.Run;
      }

      if (newVelocityY > jumpFallThreshold) {
        newAnimationState = AnimationState.Jump;
      } else if (newVelocityY < -jumpFallThreshold) {
        newAnimationState = AnimationState.Fall;
      }

      _rigidbody.velocity = new Vector2(newVelocityX, newVelocityY);

      _animationManager.SetState(newAnimationState);
    }

    private bool IsGrounded() {
      var bounds = _collider.bounds;

      return Physics2D.BoxCast(
        origin: bounds.center,
        size: bounds.size,
        angle: 0f,
        direction: Vector2.down,
        distance: 0.3f,
        layerMask: groundLayer
      );
    }
  }
}
