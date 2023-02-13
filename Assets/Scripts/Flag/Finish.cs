using Player;
using UnityEngine;

namespace Flag {
  public class Finish : MonoBehaviour {
    [SerializeField] private Lifecycle playerLifecycle;

    private void OnTriggerEnter2D(Collider2D collision) {
      playerLifecycle.EndLevel();
    }
  }
}
