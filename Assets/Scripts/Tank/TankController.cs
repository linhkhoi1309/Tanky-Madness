using System;
using UnityEngine;

public class TankController : MonoBehaviour
{

    [SerializeField] private TankMovementController movement;
    [SerializeField] private TankAimController aim;
    [SerializeField] private TankWeaponController weapon;

    private ITankInput input;

    private void Start()
    {
        input = GetComponent<ITankInput>();

        if (input == null)
        {
            Debug.LogError("Tank Input is not assigned in " + gameObject.name, this);
            enabled = false;
        }
    }

    private void Update()
    {
        movement.Move(input.Move);
        aim.Aim(input.AimDirection);

        if (input.FireTriggered) weapon.Fire();
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (movement == null) Debug.LogWarning("Movement Controller is not assigned in " + gameObject.name);
        if (aim == null) Debug.LogWarning("Aim Controller is not assigned in " + gameObject.name);
        if (weapon == null) Debug.LogWarning("Weapon Controller is not assigned in " + gameObject.name);
    }
#endif

}
