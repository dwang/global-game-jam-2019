using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceExtensions
{
    public static void Play(this AudioSource audio, AudioClip clip, float volume, float lowestPitch, float highestPitch)
    {
        audio.pitch = Random.Range(lowestPitch, highestPitch);
        audio.PlayOneShot(clip, volume);
    }

    public static void Play(this AudioSource audio, float volume, float lowestPitch, float highestPitch)
    {
        audio.pitch = Random.Range(lowestPitch, highestPitch);
        audio.PlayOneShot(audio.clip, volume);
    }

    public static void Play(this AudioSource audio, float volume, float lowestPitch, float highestPitch, float delay)
    {
        audio.pitch = Random.Range(lowestPitch, highestPitch);
        audio.volume = volume;
        audio.PlayDelayed(delay);
    }
}
