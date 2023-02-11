using UnityEngine;

namespace Player {
  public class AppleCollector : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Apple")) {
        Destroy(collision.gameObject);
      }
    }
  }
}
