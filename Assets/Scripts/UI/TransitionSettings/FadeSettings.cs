using Assets.Scripts.UI;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/Transitions/Fade")]
public class FadeSettings : TransitionSettings
{
    public float StartOpacity;
    public float EndOpacity;

    public override UITransition Create() => UITransitions.Fade(StartOpacity, EndOpacity, DurationMs);
}