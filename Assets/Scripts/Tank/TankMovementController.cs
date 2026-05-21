using UnityEngine;

public class TankMovementController : MonoBehaviour
{

    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private float moveSpeed = 5f;

    public void Move(Vector2 move)
    {
        if (move == Vector2.zero) return;

        move = Vector2.ClampMagnitude(move, 1f);

        float targetAngle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
        if (angleDifference < 13f)
        {
            transform.position += moveSpeed * Time.deltaTime * (Vector3)move;
        }
    }
}
