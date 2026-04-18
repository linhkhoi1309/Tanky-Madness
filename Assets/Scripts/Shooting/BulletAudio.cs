using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] bouncingSounds;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBounceSound()
    {
        int index = Random.Range(0, bouncingSounds.Length);
        audioSource.PlayOneShot(bouncingSounds[index]);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (bouncingSounds == null || bouncingSounds.Length == 0)
        {
            Debug.LogWarning("BulletAudio: No bouncing sounds assigned. Please assign at least one AudioClip to the bouncingSounds array.");
        }
    }
#endif
}
