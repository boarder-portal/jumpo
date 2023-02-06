using UnityEngine;

namespace Player {
  public class PlayerAnimationController : MonoBehaviour {
    private static readonly int PlayerAnimationState = Animator.StringToHash("playerAnimationState");

    private Animator _animator;

    public enum AnimationState {
      Idle,
      Run,
      Jump,
      Fall,
    }

    private void Start() {
      _animator = GetComponent<Animator>();
    }

    public void SetState(AnimationState state) {
      _animator.SetInteger(PlayerAnimationState, (int)state);
    }
  }
}