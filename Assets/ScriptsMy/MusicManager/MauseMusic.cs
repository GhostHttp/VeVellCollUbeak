using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMusicManager : MonoBehaviour
{
    public AudioSource audioSourceEffects;
    public AudioSource audioSourceAmbient;
    public AudioSource audioSourceZoomAmbient;
    public AudioSource audioSourceHRAmbient;
    public List<AudioClip> effects = new List<AudioClip>();
    public List<AudioClip> Musics = new List<AudioClip>();

    public void PlayOneShot(AudioClip clip)
    {
        audioSourceEffects.PlayOneShot(clip);
    }

    public void PlayAmbient()
    {
        audioSourceAmbient.Play();
    }
    public void PlayZoomAmbient()
    {
        audioSourceZoomAmbient.Play();
    }
}

