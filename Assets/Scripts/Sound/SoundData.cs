using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public SoundType SoundType;
    public AudioClip AudioClip;
    [Range(0, 1)]
    public float Volume;
    [HideInInspector]
    public AudioSource AudioSource;
}

public enum SoundType
{
    ButtonClick,
}
