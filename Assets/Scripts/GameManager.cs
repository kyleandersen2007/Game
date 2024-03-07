using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip countDownVoice;

    private AudioSource source;

    private void Awake()
    {
       source = GetComponent <AudioSource>();
    }

    public void PlayAudioClip(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
