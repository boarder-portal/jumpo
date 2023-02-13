using Shared.Components;

namespace Trampoline {
  public enum AnimationState {
    Idle,
    Active,
  }

  public class AnimationManager : AnimationManager<AnimationState> {
    protected override AnimationState DefaultState => AnimationState.Idle;
  }
}
