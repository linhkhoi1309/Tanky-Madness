using System;
using UnityEngine;

public class TankWeaponController : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    public event Action Fired;

    public void Fire()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Move(firePoint.up);
                Fired?.Invoke();
            }
        }
        else
        {
            Debug.LogWarning("Cannot shoot: Bullet Prefab or Fire Point is not assigned in " + gameObject.name);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (bulletPrefab == null) Debug.LogWarning("Bullet Prefab is not assigned in " + gameObject.name);
        if (firePoint == null) Debug.LogWarning("Fire Point is not assigned in " + gameObject.name);
    }
#endif

}
