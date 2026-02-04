using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset inputActions;

    public float moveSpeed = 5f;
    public float rotateSpeed = 200f;

    private InputAction moveAction;
    private InputAction rotateAction;
    private InputAction shootAction;
    private float moveInput;
    private float rotateInput;

    private void Awake() {
        moveAction = InputSystem.actions.FindAction("Move");
        rotateAction = InputSystem.actions.FindAction("Rotate");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    void Start()
    {
        
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<float>();
        rotateInput = rotateAction.ReadValue<float>();
        transform.Translate(Vector3.up * moveInput * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward, rotateInput * rotateSpeed * Time.deltaTime);
    }
}
