using UnityEngine;
using UnityEngine.InputSystem;

public class PcTankInput : MonoBehaviour, ITankInput
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private Transform aimOrigin;
    [SerializeField] private Camera mainCamera;

    public Vector2 Move { get; private set; }
    public Vector2 AimDirection { get; private set; }
    public bool FireTriggered { get; private set; }

    private void OnEnable()
    {
        EnableActions();
    }

    private void OnDisable()
    {
        DisableActions();
    }

    public void Configure(InputActionReference moveAction, InputActionReference fireAction, Transform aimOrigin, Camera mainCamera)
    {
        DisableActions();

        this.moveAction = moveAction;
        this.fireAction = fireAction;
        this.aimOrigin = aimOrigin;
        this.mainCamera = mainCamera;

        if (isActiveAndEnabled)
        {
            EnableActions();
        }
    }

    private void Update()
    {
        if (moveAction == null || fireAction == null || aimOrigin == null || mainCamera == null || Pointer.current == null)
        {
            Move = Vector2.zero;
            AimDirection = Vector2.zero;
            FireTriggered = false;
            return;
        }

        Move = moveAction.action.ReadValue<Vector2>();

        Vector2 pointerScreen = Pointer.current.position.ReadValue();
        Vector3 pointerWorld = mainCamera.ScreenToWorldPoint(pointerScreen);
        pointerWorld.z = aimOrigin.position.z;

        AimDirection = ((Vector2)(pointerWorld - aimOrigin.position)).normalized;

        FireTriggered = fireAction.action.WasPressedThisFrame();
    }

    private void EnableActions()
    {
        if (moveAction != null) moveAction.action.Enable();
        if (fireAction != null) fireAction.action.Enable();
    }

    private void DisableActions()
    {
        if (moveAction != null) moveAction.action.Disable();
        if (fireAction != null) fireAction.action.Disable();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (moveAction == null) Debug.LogWarning("Move Action is not assigned in " + gameObject.name);
        if (fireAction == null) Debug.LogWarning("Fire Action is not assigned in " + gameObject.name);
        if (aimOrigin == null) Debug.LogWarning("Aim Origin is not assigned in " + gameObject.name);
        if (mainCamera == null) Debug.LogWarning("Main Camera is not assigned in " + gameObject.name);
    }
#endif

}
