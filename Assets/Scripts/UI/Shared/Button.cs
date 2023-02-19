using UnityEngine;

namespace UI.Shared {
  public class Button : MonoBehaviour {
    [SerializeField] private AudioSource clickSound;

    public void OnClick() {
      clickSound.Play();
    }
  }
}
