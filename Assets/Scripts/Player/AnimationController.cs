using UnityEngine;

namespace Player {
  public enum AnimationState {
    Idle,
    Run,
    Jump,
    Fall,
  }

  public class AnimationController : MonoBehaviour {
    private static readonly int PlayerAnimationState = Animator.StringToHash("playerAnimationState");

    private Animator _animator;

    private void Start() {
      _animator = GetComponent<Animator>();
    }

    public void SetState(AnimationState state) {
      _animator.SetInteger(PlayerAnimationState, (int)state);
    }
  }
}
