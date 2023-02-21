using Core;
using UnityEngine;

namespace Flag {
  public class Finish : MonoBehaviour {
    [SerializeField] private LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collision) {
      levelManager.CompleteLevel();
    }
  }
}
