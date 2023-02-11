using UnityEngine;
using TMPro;

namespace Player {
  public class AppleCollector : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI counterText;

    private int _applesCount;

    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Apple")) {
        Destroy(collision.gameObject);

        _applesCount++;

        counterText.text = $"{_applesCount}";
      }
    }
  }
}
