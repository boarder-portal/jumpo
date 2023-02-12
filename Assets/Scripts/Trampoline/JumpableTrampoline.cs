using Player;
using UnityEngine;

namespace Trampoline {
  public class JumpableTrampoline : MonoBehaviour {
    [SerializeField] private GameObject platform;
    [SerializeField] private Controller playerController;
    [SerializeField] private float jumpForce = 25f;

    private AnimationManager _animationManager;

    private void Start() {
      _animationManager = GetComponent<AnimationManager>();
    }

    public void Activate() {
      playerController.StickTo(platform.transform);
      _animationManager.SetState(AnimationState.Active);
    }

    public void ActivateJump() {
      playerController.Unstick(platform.transform);
      playerController.ExternalJump(jumpForce);
    }

    public void ToIdle() {
      _animationManager.SetState(AnimationState.Idle);
    }
  }
}
