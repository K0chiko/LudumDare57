using System;
using UnityEngine;
using UnityEngine.Audio;


public enum SoundType
{
    ERROR,
    SUCCESS,
    JETPACK,
    PICKUP,
    BUTTONCLICK,
    BREATH
}

[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance = null;
    private AudioSource audioSource;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
    }


    public static void PlaySound(SoundType sound, float volume = 1f)
    {


        //SoundList soundList = instance.SO.sounds[(int)sound];
        AudioClip[] clips = instance.soundList[(int)sound].sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip);


    }

    public static AudioClip PlayBreathSound(SoundType sound, int currentClipIndex)
    {
        AudioClip[] clips = instance.soundList[(int)sound].sounds;
        return clips[currentClipIndex];
    }
}



[Serializable]
public struct SoundList
{
    [SerializeField] private string name;
    public AudioClip[] sounds;
    /*    [HideInInspector] public string name;
        [Range(0, 1)] public float volume;
        public AudioMixerGroup mixer;
        public AudioClip[] sounds;*/
}

