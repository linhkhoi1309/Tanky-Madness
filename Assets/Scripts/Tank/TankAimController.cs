using UnityEngine;

public class TankAimController : MonoBehaviour
{

    [SerializeField] private Transform turret;
    [SerializeField] private float rotateSpeed = 180f;

    public void Aim(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01f) return;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        turret.rotation = Quaternion.RotateTowards(turret.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

}
