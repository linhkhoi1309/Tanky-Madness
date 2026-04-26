using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] private AudioClip powerupPickupClip;
    private AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlayShootingSound() {
        if(shootingClip == null) {
            Debug.LogWarning("Shooting clip is not assigned; cannot play shooting sound.");
            return;
        }
        audioSource.PlayOneShot(shootingClip);
    }

    public void PlayPowerupPickupSound() {
        if(powerupPickupClip == null) {
            Debug.LogWarning("Powerup pickup clip is not assigned; cannot play pickup sound.");
            return;
        }
        audioSource.PlayOneShot(powerupPickupClip);
    }

    #if UNITY_EDITOR
    private void OnValidate() {
        if(shootingClip == null) {
            Debug.LogWarning("Shooting clip is not assigned in the inspector.");
        }
        if(powerupPickupClip == null) {
            Debug.LogWarning("Powerup pickup clip is not assigned in the inspector.");
        }
    }
    #endif
}
