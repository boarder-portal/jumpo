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

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float sideForce = 7f;
    [SerializeField] private float jumpFallThreshold = 0.001f;
    [SerializeField] private float lowBound = -5f;

    private Rigidbody2D _rigidbody;
    private AnimationManager _animationManager;
    private SpriteRenderer _sprite;
    private Collider2D _collider;
    private Lifecycle _lifecycle;
    private Transform _playerParent;

    private void Start() {
      _rigidbody = GetComponent<Rigidbody2D>();
      _animationManager = GetComponent<AnimationManager>();
      _sprite = GetComponent<SpriteRenderer>();
      _collider = GetComponent<Collider2D>();
      _lifecycle = GetComponent<Lifecycle>();
      _playerParent = transform.parent;
    }

    private void Update() {
      if (transform.position.y < lowBound) {
        _lifecycle.Die();
      }

      if (!_lifecycle.IsControllable) {
        return;
      }

      var newAnimationState = AnimationState.Idle;
      var newVelocity = GetNewVelocity();

      _rigidbody.velocity = newVelocity;

      if (newVelocity.x != 0) {
        newAnimationState = AnimationState.Run;
      }

      if (newVelocity.y > jumpFallThreshold) {
        newAnimationState = AnimationState.Jump;
      } else if (newVelocity.y < -jumpFallThreshold) {
        newAnimationState = AnimationState.Fall;
      }

      _animationManager.SetState(newAnimationState);
    }

    private Vector2 GetNewVelocity() {
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
        layerMask: jumpableGround
      );
    }

    public void ExternalJump(float externalJumpForce) {
      _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, externalJumpForce);
    }

    public void StickTo(Transform t) {
      transform.SetParent(t);
    }

    public void Unstick(Transform t) {
      if (transform.parent == t) {
        transform.SetParent(_playerParent);
      }
    }
  }
}
