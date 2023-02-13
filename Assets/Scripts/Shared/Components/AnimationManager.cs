using System;
using UnityEngine;

namespace Shared.Components {
  public abstract class AnimationManager<TAnimation> : MonoBehaviour where TAnimation : IConvertible {
    [SerializeField] private AnimationClip[] animations;

    private Animator _animator;
    private int _stateValue;

    protected abstract TAnimation DefaultState { get; }

    private void Start() {
      _animator = GetComponent<Animator>();
      _stateValue = (int)(object)DefaultState;
    }

    public void SetState(TAnimation state) {
      if (!typeof(TAnimation).IsEnum) {
        Debug.LogWarning("Passed wrong argument to SetState()");

        return;
      }

      var newStateValue = (int)(object)state;

      if (newStateValue == _stateValue) {
        return;
      }

      _stateValue = newStateValue;

      _animator.Play(animations[_stateValue].name);
    }
  }
}