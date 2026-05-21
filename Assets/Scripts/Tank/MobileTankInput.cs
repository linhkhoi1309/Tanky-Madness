using UnityEngine;
using UnityEngine.InputSystem;

public class MobileTankInput : MonoBehaviour, ITankInput
{

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference aimAction;

    public Vector2 Move { get; private set; }
    public Vector2 AimDirection { get; private set; }
    public bool FireTriggered { get; private set; }

    private bool wasAiming;
    private Vector2 lastAimDirection = Vector2.right;

    private void OnEnable()
    {
        EnableActions();
    }

    private void OnDisable()
    {
        DisableActions();
    }

    public void Configure(InputActionReference moveAction, InputActionReference aimAction)
    {
        DisableActions();

        this.moveAction = moveAction;
        this.aimAction = aimAction;

        if (isActiveAndEnabled)
        {
            EnableActions();
        }
    }

    private void Update()
    {
        if (moveAction == null || aimAction == null)
        {
            Move = Vector2.zero;
            FireTriggered = false;
            return;
        }

        Move = moveAction.action.ReadValue<Vector2>();

        Vector2 rawAim = aimAction.action.ReadValue<Vector2>();
        bool isAiming = rawAim.magnitude > 0.1306f;

        FireTriggered = false;

        if (isAiming)
        {
            lastAimDirection = rawAim.normalized;
            AimDirection = lastAimDirection;
            wasAiming = true;
        }
        else
        {
            AimDirection = lastAimDirection;

            if (wasAiming)
            {
                FireTriggered = true;
                wasAiming = false;
            }
        }
    }

    private void EnableActions()
    {
        if (moveAction != null) moveAction.action.Enable();
        if (aimAction != null) aimAction.action.Enable();
    }

    private void DisableActions()
    {
        if (moveAction != null) moveAction.action.Disable();
        if (aimAction != null) aimAction.action.Disable();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (moveAction == null) Debug.LogWarning("Move Action is not assigned in " + gameObject.name);
        if (aimAction == null) Debug.LogWarning("Aim Action is not assigned in " + gameObject.name);
    }
#endif

}
