using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    private AudioSource source;
    
    [SerializeField] private AudioClip[] clips;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }


    private AudioClip TakeRandomStepSound()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void PlayStepSound()
    {
        source.PlayOneShot(TakeRandomStepSound());
    }
}
