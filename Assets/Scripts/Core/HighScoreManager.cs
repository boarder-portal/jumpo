using System;
using System.Collections.Generic;
using Shared.Utilities;

namespace Core {
  [DataPath("/highscores")]
  [Serializable]
  public class HighScoreManager : PersistentDataManager<HighScoreManager> {
    private readonly Dictionary<int, float> _scores = new();

    public float GetLevelScore(int level) {
      return _scores.ContainsKey(level) ? _scores[level] : float.PositiveInfinity;
    }

    public void TrySetLevelScore(int level, float score) {
      var currentScore = GetLevelScore(level);

      if (currentScore <= score) {
        return;
      }

      _scores[level] = score;

      Save();
    }
  }
}
