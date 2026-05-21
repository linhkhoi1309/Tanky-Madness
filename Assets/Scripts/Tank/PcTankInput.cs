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
        moveAction.action.Enable();
        fireAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        fireAction.action.Disable();
    }

    private void Update()
    {
        Move = moveAction.action.ReadValue<Vector2>();

        Vector2 pointerScreen = Pointer.current.position.ReadValue();
        Vector3 pointerWorld = mainCamera.ScreenToWorldPoint(pointerScreen);
        pointerWorld.z = aimOrigin.position.z;

        AimDirection = ((Vector2)(pointerWorld - aimOrigin.position)).normalized;

        FireTriggered = fireAction.action.WasPressedThisFrame();
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
