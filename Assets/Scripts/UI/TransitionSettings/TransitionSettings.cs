using Assets.Scripts.UI;
using UnityEngine;

public abstract class TransitionSettings : ScriptableObject
{
    public int DurationMs = 600;
    public abstract UITransition Create();
}
