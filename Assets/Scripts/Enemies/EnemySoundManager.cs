using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayOneTime(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
