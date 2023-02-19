using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelsList {
  public class LevelsListManager : MonoBehaviour {
    [SerializeField] private GameObject levelsList;
    [SerializeField] private GameObject selectLevelButtonPrefab;
    [SerializeField] private int rowLevelsCount = 5;
    [SerializeField] private int colLevelsCount = 3;
    [SerializeField] private Vector2 buttonsDistance = new(300, 150);
    [SerializeField] private Vector2 levelsOffset = new(0, 200);

    private void Awake() {
      var levelsCount = CoreAPI.SceneManager.GetLevelCount();
      var halfX = (rowLevelsCount - 1) / 2f;
      var halfY = (colLevelsCount - 1) / 2f;

      for (var i = 0; i < levelsCount; i++) {
        var levelIndex = i + 1;
        var x = i % rowLevelsCount;
        var y = i / rowLevelsCount;
        var buttonObject = Instantiate(selectLevelButtonPrefab, levelsList.transform);
        var button = buttonObject.GetComponent<Button>();
        var buttonText = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
        var rectTransform = buttonObject.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(
          levelsOffset.x + (x - halfX) * buttonsDistance.x,
          levelsOffset.y - (y - halfY) * buttonsDistance.y
        );

        buttonText.SetText($"{levelIndex}");

        button.onClick.AddListener(() => {
          CoreAPI.SceneManager.GoToLevel(levelIndex);
        });
      }
    }

    public void GoToMainMenu() {
      CoreAPI.SceneManager.LoadMainMenu();
    }
  }
}
