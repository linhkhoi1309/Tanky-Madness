using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Explosion : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem m_fragmentParticles;

    [SerializeField]
    private ParticleSystem m_smokeParticles;

    private AudioSource m_explosionAudio;

    private void Awake()
    {
        m_explosionAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        m_explosionAudio.Play();
        m_fragmentParticles.Play();
        m_smokeParticles.Play();
    }
}
