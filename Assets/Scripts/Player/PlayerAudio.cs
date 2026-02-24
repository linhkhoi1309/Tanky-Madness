using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip shootingClip;
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

    #if UNITY_EDITOR
    private void OnValidate() {
        if(shootingClip == null) {
            Debug.LogWarning("Shooting clip is not assigned in the inspector.");
        }
    }
    #endif
}
