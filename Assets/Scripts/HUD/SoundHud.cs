using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHud : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;

    public void PlayClip(AudioClip clip)
    {
        m_AudioSource.PlayOneShot(clip);
    }
}
