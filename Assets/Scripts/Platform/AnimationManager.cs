using System;
using UnityEngine;

namespace Platform {
  public class AnimationManager : MonoBehaviour {
    private static readonly int AnimationMultiplier = Animator.StringToHash("animationMultiplier");

    private Animator _animator;
    private Vector2 _prevPosition;

    private void Start() {
      _animator = GetComponent<Animator>();
      _prevPosition = transform.position;
    }

    private void Update() {
      _animator.SetFloat(AnimationMultiplier, Math.Sign(transform.position.x - _prevPosition.x));

      _prevPosition = transform.position;
    }
  }
}