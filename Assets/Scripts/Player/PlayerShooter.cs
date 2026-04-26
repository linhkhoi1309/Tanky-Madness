using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    public void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            GameObject bulletInstance = ObjectPool.Instance.SpawnFromPool("Bullet", firePoint.position, Vector3.one, firePoint.rotation);
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetOwner(gameObject);
                bullet.Move(GetShootingDirectionVector().normalized);
            }
        }
        else
        {
            Debug.LogWarning("Cannot shoot: Bullet Prefab or Fire Point is not assigned in " + gameObject.name);
        }
    }

    private Vector2 GetShootingDirectionVector()
    {
        Vector2 shootingDirection = firePoint.position - transform.position;
        return shootingDirection;
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (bulletPrefab == null) Debug.LogWarning("Bullet Prefab is not assigned in " + gameObject.name);
        if (firePoint == null) Debug.LogWarning("Fire Point is not assigned in " + gameObject.name);
    }
#endif
}
