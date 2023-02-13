using Player;
using UnityEngine;

namespace Trampoline {
  public class JumpableTrampoline : MonoBehaviour {
    [SerializeField] private GameObject platform;
    [SerializeField] private Controller playerController;
    [SerializeField] private float jumpForce = 25f;

    private AnimationManager _animationManager;
    private AudioManager _audioManager;

    private void Start() {
      _animationManager = GetComponent<AnimationManager>();
      _audioManager = GetComponent<AudioManager>();
    }

    public void Activate() {
      playerController.StickTo(platform.transform);
      _animationManager.SetState(AnimationState.Active);
    }

    public void ActivateJump() {
      _audioManager.Play(Audio.Activate);

      playerController.Unstick(platform.transform);
      playerController.ExternalJump(jumpForce);
    }

    public void ToIdle() {
      _animationManager.SetState(AnimationState.Idle);
    }
  }
}
