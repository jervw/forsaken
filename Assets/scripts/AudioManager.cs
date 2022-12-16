using System;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// This is static class for audio management in the game. 
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer mixer;
    public Sound[] sounds;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        LoadSounds();
    }

    /// <summary>
    /// Loads all sounds from the sounds array into the audio mixer.
    /// </summary>
    void LoadSounds()
    {
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = mixer.FindMatchingGroups(s.type.ToString())[0];
        }
    }

    /// <summary>
    /// Plays a sound by the the track name. Given track must be in the sounds array.
    /// </summary>
    /// <param name="trackName"></param>
    public void Play(string trackName)
    {
        var sound = Array.Find(sounds, sound => sound.clip.name == trackName);

        if (sound)
        {
            sound.source.Play();
        }
        else
        {
            Debug.LogWarning($"Sound: {trackName} not found!");
        }
    }

    /// <summary>
    /// Pause the sound by the track name. Given track must be in the sounds array.
    /// </summary>
    /// <param name="trackName"></param>
    public void Pause(string trackName)
    {
        var sound = Array.Find(sounds, sound => sound.clip.name == trackName);

        if (sound)
        {
            sound.source.Pause();
        }
        else
        {
            Debug.LogWarning($"Sound {trackName} not found!");
        }
    }
    
    /// <summary>
    /// Stops the sound given by the track name. Given track must be in the sounds array.
    /// </summary>
    /// <param name="trackName"></param>
    public void Stop(string trackName)
    {
        var sound = Array.Find(sounds, sound => sound.clip.name == trackName);

        if (sound)
        {
            sound.source.Stop();
        }
        else
        {
            Debug.LogWarning($"Sound {trackName} not found!");
        }
    }
}