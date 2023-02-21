using UnityEngine;

namespace Background {
  public class Manager : MonoBehaviour {
    [SerializeField] private SpriteRenderer leftmostImage;
    [SerializeField] private SpriteRenderer rightmostImage;
    [SerializeField] private new UnityEngine.Camera camera;
    [SerializeField] private float changeThreshold = 0.05f;

    private float _imageLength;
    private float _cameraWidth;

    private void Start() {
      _imageLength = leftmostImage.bounds.size.x;
      _cameraWidth = 2 * camera.orthographicSize * camera.aspect;
    }

    private void Update() {
      var cameraPosition = camera.transform.position;

      var leftBackgroundX = leftmostImage.transform.position.x - _imageLength / 2;
      var leftCameraX = cameraPosition.x - _cameraWidth / 2;

      var rightBackgroundX = rightmostImage.transform.position.x + _imageLength / 2;
      var rightCameraX = cameraPosition.x + _cameraWidth / 2;

      float positionChangeX = 0;

      if (leftCameraX - leftBackgroundX < _imageLength * changeThreshold) {
        positionChangeX = -_imageLength;
      } else if (rightBackgroundX - rightCameraX < _imageLength * changeThreshold) {
        positionChangeX = _imageLength;
      }

      if (positionChangeX != 0) {
        var position = transform.position;

        transform.position = new Vector3(position.x + positionChangeX, position.y, position.z);
      }
    }
  }
}
