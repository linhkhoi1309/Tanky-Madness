using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TankAudio : MonoBehaviour
{

    [SerializeField] private TankWeaponController weapon;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fireClip;

    private void OnEnable()
    {
        weapon.Fired += HandleFire;
    }

    private void OnDisable()
    {
        weapon.Fired -= HandleFire;
    }

    private void HandleFire()
    {
        audioSource.PlayOneShot(fireClip);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (weapon == null) Debug.LogWarning("Weapon Controller is not assigned in " + gameObject.name);
        if (audioSource == null) Debug.LogWarning("Audio Source is not assigned in " + gameObject.name);
        if (fireClip == null) Debug.LogWarning("Fire Audio Clip is not assigned in " + gameObject.name);
    }
#endif

}
