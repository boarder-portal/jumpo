using System;
using UnityEngine;

namespace Shared.Components {
  public class AudioManager<TAudio> : MonoBehaviour where TAudio : IConvertible {
    [SerializeField] private GameObject audioSources;

    private AudioSource[] _audioSources;

    private void Start() {
      _audioSources = audioSources.GetComponentsInChildren<AudioSource>();
    }

    public void Play(TAudio sourceType) {
      if (!typeof(TAudio).IsEnum) {
        Debug.LogWarning("Passed wrong argument to Play()");

        return;
      }

      _audioSources[(int)(object)sourceType].Play();
    }
  }
}
