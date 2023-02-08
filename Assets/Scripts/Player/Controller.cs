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

    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float sideForce = 7f;
    [SerializeField] private float jumpFallThreshold = 0.001f;

    private Rigidbody2D _rigidbody;
    private AnimationController _animationController;
    private SpriteRenderer _sprite;

    private void Start() {
      _rigidbody = GetComponent<Rigidbody2D>();
      _animationController = GetComponent<AnimationController>();
      _sprite = GetComponent<SpriteRenderer>();
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

      if (Input.GetKeyDown("space")) {
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

      _animationController.SetState(newAnimationState);
    }
  }
}
