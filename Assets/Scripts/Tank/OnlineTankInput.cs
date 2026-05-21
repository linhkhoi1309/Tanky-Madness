using UnityEngine;

public class OnlineTankInput : MonoBehaviour, ITankInput
{
    public Vector2 Move { get; private set; }
    public Vector2 AimDirection { get; private set; } = Vector2.up;
    public bool FireTriggered { get; private set; }

    public void SetInput(Vector2 move, Vector2 aimDirection, bool fireTriggered)
    {
        Move = Vector2.ClampMagnitude(move, 1f);

        if (aimDirection.sqrMagnitude > 0.01f)
        {
            AimDirection = aimDirection.normalized;
        }

        FireTriggered = fireTriggered;
    }

    private void LateUpdate()
    {
        FireTriggered = false;
    }
}
