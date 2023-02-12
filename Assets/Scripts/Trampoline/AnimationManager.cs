using Shared.Utilities;

namespace Trampoline {
  public enum AnimationState {
    Idle,
    Active,
  }

  public class AnimationManager : AnimationManager<AnimationState> {
    protected override AnimationState DefaultState => AnimationState.Idle;
  }
}
