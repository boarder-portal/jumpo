using UnityEngine;

namespace UI {
  public class Button : MonoBehaviour {
    [SerializeField] private AudioSource clickSound;

    public void OnClick() {
      clickSound.Play();
    }
  }
}
