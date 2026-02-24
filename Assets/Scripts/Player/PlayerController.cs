using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput)), 
RequireComponent(typeof(PlayerMovement)), 
RequireComponent(typeof(PlayerShooter)),
RequireComponent(typeof(PlayerAudio))]
public class PlayerController : MonoBehaviour
{
    private float moveInput;
    private float rotateInput;
    private Vector2 mouseScreenPos;
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;
    private PlayerAudio playerAudio;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
        playerAudio = GetComponent<PlayerAudio>();
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
        playerShooter.Shoot();
        playerAudio.PlayShootingSound();
    }

    public void OnPoint(InputValue value)
    {
        mouseScreenPos = value.Get<Vector2>();
    }

    void Update()
    {
        if (playerInput.currentControlScheme == "Mouse_Scheme") playerMovement.HandleMouseMovement(mouseScreenPos);
        else playerMovement.HandleKeyboardMovement(moveInput, rotateInput);
    }
}
