using System;
using UnityEngine;

namespace Shared.Components {
  public class AudioManager<TAudio> : MonoBehaviour where TAudio : IConvertible {
    [SerializeField] private GameObject audioSources;

    private AudioSource[] _audioSources;

    private void Start() {
      _audioSources = audioSources.GetComponentsInChildren<AudioSource>();

      if (!typeof(TAudio).IsEnum) {
        throw new Exception("Wrong TAudio");
      }

      var expectedClipsCount = Enum.GetNames(typeof(TAudio)).Length;
      var actualClipsCount = _audioSources.Length;

      if (expectedClipsCount != actualClipsCount) {
        throw new Exception($"Wrong number of audio clips (expected {expectedClipsCount}, got {actualClipsCount})");
      }
    }

    public void Play(TAudio sourceType) {
      _audioSources[(int)(object)sourceType].Play();
    }
  }
}
