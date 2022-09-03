using System;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;
    [SerializeField] private Sound[] _sounds;

    public void PlaySound(SoundType soundType)
    {
        var sound = Array.Find(_sounds, s => s.SoundType == soundType);
        sound.AudioSource.Play();
    }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        InitializeSounds();
    }

    private void InitializeSounds()
    {
        foreach (var sound in _sounds)
        {
            sound.AudioSource = gameObject.AddComponent<AudioSource>();
            sound.AudioSource.clip = sound.AudioClip;
            sound.AudioSource.volume = sound.Volume;
        }
    }
}
