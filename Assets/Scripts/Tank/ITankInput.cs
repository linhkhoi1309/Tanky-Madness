using UnityEngine;

public interface ITankInput
{

    public Vector2 Move { get; }
    public Vector2 AimDirection { get; }
    public bool FireTriggered { get; }

}
