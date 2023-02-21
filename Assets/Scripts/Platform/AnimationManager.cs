using System;
using UnityEngine;

namespace Platform {
  [RequireComponent(typeof(Animator))]
  public class AnimationManager : MonoBehaviour {
    private static readonly int AnimationMultiplier = Animator.StringToHash("animationMultiplier");

    private Animator _animator;
    private Vector2 _prevPosition;

    private void Start() {
      _animator = GetComponent<Animator>();
      _prevPosition = transform.position;
    }

    private void Update() {
      var currentPosition = transform.position;
      var direction = (
        Math.Sign(currentPosition.x - _prevPosition.x)
        | Math.Sign(currentPosition.y - _prevPosition.y)
      );

      _animator.SetFloat(AnimationMultiplier, direction);

      _prevPosition = currentPosition;
    }
  }
}
