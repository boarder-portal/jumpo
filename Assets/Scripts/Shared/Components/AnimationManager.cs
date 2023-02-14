using System;
using UnityEngine;

namespace Shared.Components {
  public abstract class AnimationManager<TAnimation> : MonoBehaviour where TAnimation : IConvertible {
    [SerializeField] private AnimationClip[] animations;

    private Animator _animator;
    private TAnimation _stateValue;

    protected abstract TAnimation DefaultState { get; }

    private void Start() {
      _animator = GetComponent<Animator>();
      _stateValue = DefaultState;
    }

    public TAnimation GetCurrentState() {
      return _stateValue;
    }

    public void SetState(TAnimation state) {
      if (!typeof(TAnimation).IsEnum) {
        Debug.LogWarning("Passed wrong argument to SetState()");

        return;
      }

      var newStateValue = (int)(object)state;

      if (newStateValue == (int)(object)_stateValue) {
        return;
      }

      _stateValue = state;

      _animator.Play(animations[newStateValue].name);
    }
  }
}
