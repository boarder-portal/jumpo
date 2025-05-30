using Player;
using UnityEngine;

namespace Trampoline {
  public class JumpableTrampoline : MonoBehaviour {
    [SerializeField] private GameObject platform;
    [SerializeField] private float jumpForce = 25f;

    private AnimationManager _animationManager;
    private AudioManager _audioManager;
    private bool _isScheduledToBounce;

    private void Awake() {
      _animationManager = GetComponent<AnimationManager>();
      _audioManager = GetComponent<AudioManager>();
    }

    private void LateUpdate() {
      if (_isScheduledToBounce) {
        ActivateBounce();
      }
    }

    public void Activate(GameObject attachedObject) {
      var playerController = attachedObject.GetComponent<Controller>();

      if (!playerController) {
        return;
      }

      playerController.StickTo(platform.transform);

      _animationManager.SetState(AnimationState.Active);
    }

    public void ScheduleToBounce() {
      _isScheduledToBounce = true;
    }

    private void ActivateBounce() {
      _isScheduledToBounce = false;

      _audioManager.Play(Audio.Activate);

      foreach (Transform child in platform.transform) {
        var playerController = child.gameObject.GetComponent<Controller>();

        if (!playerController) {
          continue;
        }

        playerController.Unstick(platform.transform);
        playerController.ExternalJump(jumpForce);
      }
    }

    public void ToIdle() {
      _animationManager.SetState(AnimationState.Idle);
    }
  }
}
