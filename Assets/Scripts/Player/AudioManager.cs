using Shared.Components;

namespace Player {
  public enum Audio {
    Jump,
    Collect,
    Death,
    Finish,
  }

  public class AudioManager : AudioManager<Audio> {}
}
