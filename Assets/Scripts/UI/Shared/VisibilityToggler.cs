using UnityEngine;

namespace UI.Shared {
  [RequireComponent(typeof(CanvasGroup))]
  public class VisibilityToggler : MonoBehaviour {
    private CanvasGroup _canvasGroup;

    public void SetVisible(bool visible) {
      _canvasGroup.alpha = visible ? 1 : 0;
      _canvasGroup.interactable = visible;
      _canvasGroup.blocksRaycasts = visible;
    }
  }
}
