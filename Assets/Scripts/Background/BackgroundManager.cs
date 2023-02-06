using UnityEngine;

namespace Background {
  public class BackgroundManager : MonoBehaviour {
    [SerializeField] private SpriteRenderer leftmostImage;
    [SerializeField] private SpriteRenderer rightmostImage;
    [SerializeField] private new Camera camera;

    private const float ChangeThreshold = 0.1f;

    private float _imageLength;
    private float _cameraWidth;

    private void Start() {
      _imageLength = leftmostImage.bounds.size.x;
      _cameraWidth = 2 * camera.orthographicSize * camera.aspect;
    }

    private void Update() {
      float cameraPositionX = camera.transform.position.x;

      float leftBackgroundX = leftmostImage.transform.position.x - _imageLength / 2;
      float leftCameraX = cameraPositionX - _cameraWidth / 2;

      float rightBackgroundX = rightmostImage.transform.position.x + _imageLength / 2;
      float rightCameraX = cameraPositionX + _cameraWidth / 2;

      float positionChange = 0;

      if (leftCameraX - leftBackgroundX < _imageLength * ChangeThreshold) {
        positionChange = -_imageLength;
      } else if (rightBackgroundX - rightCameraX < _imageLength * ChangeThreshold) {
        positionChange = _imageLength;
      }

      if (positionChange != 0) {
        Vector3 position = transform.position;

        transform.position = new Vector3(position.x + positionChange, position.y, position.z);
      }
    }
  }
}
