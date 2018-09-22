using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource m_audioSource;

    private float m_bgmVolume = 0.15f;
    [SerializeField] private float m_sfxVolume = 0.65f;

    void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void PlaySFXOneShot(AudioClip audio)
    {
        m_audioSource.PlayOneShot(audio, m_sfxVolume);
    }
}
