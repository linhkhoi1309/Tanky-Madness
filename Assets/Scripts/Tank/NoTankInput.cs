using UnityEngine;

public class NoTankInput : MonoBehaviour, ITankInput
{

    public Vector2 Move { get; } = Vector2.zero;
    public Vector2 AimDirection { get; } = Vector2.up;
    public bool FireTriggered { get; } = false;

}
