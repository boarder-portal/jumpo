using UnityEngine;

namespace Camera {
  public class StickyCamera : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;

    private void Update() {
      var cameraPosition = transform.position;
      var playerPosition = player.transform.position;
      var positionX = followX ? playerPosition.x : cameraPosition.x;
      var positionY = followY ? playerPosition.y : cameraPosition.y;

      transform.position = new Vector3(positionX, positionY, cameraPosition.z);
    }
  }
}
