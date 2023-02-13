using System;
using UnityEngine;

namespace Shared.Utilities {
  public abstract class AnimationManager<AnimationState> : MonoBehaviour where AnimationState : IConvertible {
    private static readonly int AnimationStateHash = Animator.StringToHash("animationState");

    private Animator _animator;
    private int _stateValue;

    protected abstract AnimationState DefaultState { get; }

    private void Start() {
      _animator = GetComponent<Animator>();
      _stateValue = (int)(object)DefaultState;
    }

    public void SetState(AnimationState state) {
      if (!typeof(AnimationState).IsEnum) {
        return;
      }

      var newStateValue = (int)(object)state;

      if (newStateValue == _stateValue) {
        return;
      }

      _stateValue = newStateValue;

      _animator.SetInteger(AnimationStateHash, _stateValue);
    }
  }
}