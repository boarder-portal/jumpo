using System.Collections.Generic;
using UnityEngine;

namespace Shared.Components {
  public class WaypointFollower : MonoBehaviour {
    [SerializeField] private GameObject waypointsContainer;
    [SerializeField] private int startWaypointIndex;
    [SerializeField] private float speed = 2;

    private readonly List<GameObject> _waypoints = new();
    private int _currentWaypointIndex;

    private void Start() {
      for (var i = 0; i < waypointsContainer.transform.childCount; i++) {
        _waypoints.Add(waypointsContainer.transform.GetChild(i).gameObject);
      }

      _currentWaypointIndex = startWaypointIndex;
    }

    private void Update() {
      if (Vector2.Distance(GetCurrentWaypointPosition(), transform.position) < 0.1f) {
        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
      }

      transform.position = Vector2.MoveTowards(
        transform.position,
        GetCurrentWaypointPosition(),
        Time.deltaTime * speed
      );
    }

    private Vector2 GetCurrentWaypointPosition() {
      return _waypoints[_currentWaypointIndex].transform.position;
    }
  }
}
