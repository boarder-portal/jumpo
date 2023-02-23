using System;
using UnityEngine;

namespace Apple {
  public class Lifecycle : MonoBehaviour {
    public static event Action OnSpawn;

    private void Start() {
      OnSpawn?.Invoke();
    }
  }
}
