using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.3f, 1f)]
    public float pitch;

    public bool loop;
    public bool playOnAwake;

    [HideInInspector]
    public AudioSource source;

    public AudioMixerGroup group;
}
