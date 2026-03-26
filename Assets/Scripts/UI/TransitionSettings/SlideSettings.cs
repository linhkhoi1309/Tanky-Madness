using Assets.Scripts.UI;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/Transitions/Slide")]
public class SlideSettings : TransitionSettings
{
    public Vector2 Start;
    public Vector2 End;

    public override UITransition Create() => UITransitions.SlideRelative(Start, End, DurationMs);
}