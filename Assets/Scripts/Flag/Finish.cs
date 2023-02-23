using Core;
using UnityEngine;

namespace Flag {
  public class Finish : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
      LevelManager.Current.CompleteLevel();
    }
  }
}
