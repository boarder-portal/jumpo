using Core;
using UnityEngine;

namespace UI.Shared {
  public class Button : MonoBehaviour {
    public void OnClick() {
      CoreAPI.UIAudioManager.Play(Audio.ButtonClick);
    }
  }
}
