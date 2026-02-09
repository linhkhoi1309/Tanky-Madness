using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]

    [Tooltip("Movement speed in units per second"), Min(0f)]
    public float moveSpeed = 5f;

    [Tooltip("Rotation speed in degrees per second"), Min(0f)]
    public float rotateSpeed = 200f;

    [Tooltip("Distance to target at which to stop moving"), Min(0f)]
    public float stoppingDistance = 0.2f;
    private float moveInput;
    private float rotateInput;
    private Vector2 mouseScreenPos;
    private Camera mainCamera;
    private PlayerInput playerInput;

    void Awake()
    {
        mainCamera = Camera.main;
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
    }

    public void OnRotate(InputValue value)
    {
        rotateInput = value.Get<float>();
    }

    public void OnShoot()
    {
        Debug.Log("Shoot button pressed by " + gameObject.name);
    }

    public void OnPoint(InputValue value)
    {
        mouseScreenPos = value.Get<Vector2>();
    }

    void Update()
    {
        if (playerInput.currentControlScheme == "Mouse_Scheme") HandleMouseMovement();
        else HandleKeyboardMovement();
    }

    void HandleMouseMovement()
    {
        // 1. Convert mouse screen pixels to a world position
        Vector3 worldTarget = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f));
        worldTarget.z = transform.position.z; // Flatten to 2D plane

        // 2. Calculate Direction
        Vector3 direction = worldTarget - transform.position;
        float distance = direction.magnitude;

        if (distance > stoppingDistance)
        {
            // 3. Rotation 
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

            // Rotate toward the mouse over time
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // 4. Movement (Only move forward if we are somewhat facing the target)
            // This prevents the tank from sliding sideways while turning
            float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
            if (angleDifference < 45f)
            {
                transform.position = Vector3.MoveTowards(transform.position, worldTarget, moveSpeed * Time.deltaTime);
            }
        }
    }

    void HandleKeyboardMovement()
    {
        transform.Translate(Vector3.up * moveInput * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward, rotateInput * rotateSpeed * Time.deltaTime);
    }
}
