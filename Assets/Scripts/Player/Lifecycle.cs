using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player {
  public class Lifecycle : MonoBehaviour {
    private static string GetCurrentScene() {
      return SceneManager.GetActiveScene().name;
    }

    private Rigidbody2D _rigidbody;
    private AnimationManager _animationManager;
    private AudioManager _audioManager;

    private bool _isAlive = true;
    private bool _levelCompleted;

    public bool IsControllable { get; private set; } = true;

    private void Start() {
      _rigidbody = GetComponent<Rigidbody2D>();
      _animationManager = GetComponent<AnimationManager>();
      _audioManager = GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
      if (_isAlive && collision.gameObject.CompareTag("Spikes")) {
        Die();
      }
    }

    public void Die() {
      if (!_isAlive) {
        return;
      }

      _isAlive = false;
      IsControllable = false;

      _rigidbody.bodyType = RigidbodyType2D.Static;

      _audioManager.Play(Audio.Death);
      _animationManager.SetState(AnimationState.Dead);
    }

    public void EndLevel() {
      if (_levelCompleted) {
        return;
      }

      _levelCompleted = true;
      IsControllable = false;

      _audioManager.Play(Audio.Finish);
      _animationManager.SetState(AnimationState.Idle);

      Invoke(nameof(StartNextLevel), 2f);
    }

    // FIXME: temporary solution, add scene manager
    private void StartNextLevel() {
      var currentScene = GetCurrentScene();
      var nextScene = "";

      if (currentScene == "Level 1") {
        nextScene = "Level 2";
      }

      if (nextScene != "") {
        SceneManager.LoadScene(nextScene);
      }
    }

    public void RestartLevel() {
      SceneManager.LoadScene(GetCurrentScene());
    }
  }
}
