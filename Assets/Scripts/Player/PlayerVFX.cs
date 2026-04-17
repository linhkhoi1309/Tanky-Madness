using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    public GameObject explosionEffectPrefab;
    private void Awake() {
        
    }

    public void PlayExplosionEffect() {
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }

    #if UNITY_EDITOR
    private void OnValidate() {
        if(explosionEffectPrefab == null) {
            Debug.LogWarning("Explosion Effect Prefab is not assigned in the inspector.");
        }
    }
    #endif
}
