using Shared.Components;

namespace Player {
  public enum AnimationState {
    Idle,
    Run,
    Jump,
    Fall,
    Dead,
  }

  public class AnimationManager : AnimationManager<AnimationState> {
    protected override AnimationState DefaultState => AnimationState.Idle;
  }
}
