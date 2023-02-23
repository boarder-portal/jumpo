using System;
using UnityEngine;

namespace Shared.Components {
  public abstract class AnimationManager<TAnimation> : MonoBehaviour where TAnimation : IConvertible {
    [SerializeField] private AnimationClip[] animations;

    private Animator _animator;
    private TAnimation _stateValue;

    protected abstract TAnimation DefaultState { get; }

    private void Awake() {
      if (!typeof(TAnimation).IsEnum) {
        throw new Exception("Wrong TAnimation");
      }

      var expectedAnimationsCount = Enum.GetNames(typeof(TAnimation)).Length;
      var actualAnimationsCount = animations.Length;

      if (expectedAnimationsCount != actualAnimationsCount) {
        throw new Exception($"Wrong number of animations (expected {expectedAnimationsCount}, got {actualAnimationsCount})");
      }

      _animator = GetComponent<Animator>();
      _stateValue = DefaultState;
    }

    public TAnimation GetCurrentState() {
      return _stateValue;
    }

    public void SetState(TAnimation state) {
      var newStateValue = (int)(object)state;

      if (newStateValue == (int)(object)_stateValue) {
        return;
      }

      _stateValue = state;

      _animator.Play(animations[newStateValue].name);
    }
  }
}
