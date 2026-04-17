using UnityEngine;

[RequireComponent(typeof(ParticleSystem)),
RequireComponent(typeof(AudioSource))]
public class Explosion : MonoBehaviour
{

    private ParticleSystem m_explosionParticles;
    private AudioSource m_explosionAudio;

    private void Awake()
    {
        m_explosionParticles = GetComponent<ParticleSystem>();
        m_explosionAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        m_explosionAudio.Play();
        m_explosionParticles.Play();
    }
}
