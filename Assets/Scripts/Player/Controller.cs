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
    private Lifecycle _lifecycle;

    private void Start() {
      _rigidbody = GetComponent<Rigidbody2D>();
      _animationManager = GetComponent<AnimationManager>();
      _sprite = GetComponent<SpriteRenderer>();
      _collider = GetComponent<Collider2D>();
      _lifecycle = GetComponent<Lifecycle>();
    }

    private void Update() {
      var newVelocity = GetNewVelocity();
      var newAnimationState = AnimationState.Idle;

      if (_lifecycle.IsAlive) {
        if (newVelocity.x != 0) {
          newAnimationState = AnimationState.Run;
        }

        if (newVelocity.y > jumpFallThreshold) {
          newAnimationState = AnimationState.Jump;
        } else if (newVelocity.y < -jumpFallThreshold) {
          newAnimationState = AnimationState.Fall;
        }
      } else {
        newAnimationState = AnimationState.Dead;
      }

      _rigidbody.velocity = newVelocity;

      _animationManager.SetState(newAnimationState);
    }

    private Vector2 GetNewVelocity() {
      if (!_lifecycle.IsAlive) {
        return new Vector2(0, 0);
      }

      var currentVelocity = _rigidbody.velocity;
      var newVelocityX = 0f;
      var newVelocityY = currentVelocity.y;

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

      return new Vector2(newVelocityX, newVelocityY);
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
