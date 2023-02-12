using System;
using UnityEngine;

namespace Shared.Utilities {
  public abstract class AnimationManager<AnimationState> : MonoBehaviour where AnimationState : IConvertible {
    private static readonly int AnimationStateHash = Animator.StringToHash("animationState");

    protected abstract AnimationState DefaultState { get; }

    private Animator _animator;
    private int _state;

    private void Start() {
      _animator = GetComponent<Animator>();
      _state = (int)(object)DefaultState;
    }

    public void SetState(AnimationState state) {
      if (!typeof(AnimationState).IsEnum) {
        return;
      }

      var stateValue = (int)(object)state;

      if (stateValue == _state) {
        return;
      }

      _state = stateValue;

      _animator.SetInteger(AnimationStateHash, _state);
    }
  }
}