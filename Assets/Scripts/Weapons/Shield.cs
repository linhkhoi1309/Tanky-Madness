using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Shield : MonoBehaviour
{
    private PlayerPowerups ownerPowerups;

    public void Initialize(PlayerPowerups owner)
    {
        ownerPowerups = owner;
    }

    public bool IsActive()
    {
        return ownerPowerups != null && ownerPowerups.HasActiveShield();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryDestroyBullet(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryDestroyBullet(other.gameObject);
    }

    private void TryDestroyBullet(GameObject other)
    {
        if (!IsActive())
        {
            return;
        }

        Bullet bullet = other.GetComponentInParent<Bullet>();
        if (bullet == null)
        {
            return;
        }

        if (ownerPowerups != null && bullet.IsOwnedBy(ownerPowerups.gameObject))
        {
            return;
        }

        bullet.Disable();
    }
}
