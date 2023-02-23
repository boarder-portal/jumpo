using Core;
using UnityEngine;

namespace Player {
  public class Lifecycle : MonoBehaviour {
    [SerializeField] private float colliderOverlapThreshold = 0.3f;

    private Rigidbody2D _rigidbody;
    private AnimationManager _animationManager;
    private AudioManager _audioManager;

    private bool _isAlive = true;

    public bool IsControllable => _isAlive && !LevelManager.Current.IsCompleted && !LevelManager.Current.IsPaused;

    private void Awake() {
      _rigidbody = GetComponent<Rigidbody2D>();
      _animationManager = GetComponent<AnimationManager>();
      _audioManager = GetComponent<AudioManager>();
    }

    private void OnEnable() {
      LevelManager.OnCompleteLevel += EndLevel;
    }

    private void OnDisable() {
      LevelManager.OnCompleteLevel -= EndLevel;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      if (_isAlive && collision.gameObject.CompareTag("Spikes")) {
        Die();
      }
    }

    private void OnCollisionStay2D(Collision2D collision) {
      if (!_isAlive || collision.gameObject.CompareTag("Trampoline Platform")) {
        return;
      }

      foreach (var contact in collision.contacts) {
        if (contact.separation < -colliderOverlapThreshold) {
          Die();
        }
      }
    }

    public void Die() {
      if (!_isAlive) {
        return;
      }

      _isAlive = false;

      _rigidbody.bodyType = RigidbodyType2D.Static;

      _audioManager.Play(Audio.Death);
      _animationManager.SetState(AnimationState.Dead);
    }

    private void EndLevel() {
      _audioManager.Play(Audio.Finish);
      _animationManager.SetState(AnimationState.Idle);
    }

    public void RestartLevel() {
      LevelManager.Current.RestartLevel();
    }
  }
}
