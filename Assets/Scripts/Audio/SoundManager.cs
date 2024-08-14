using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            _audioSource = GetComponent<AudioSource>();
        }
    }

    public static void PlaySound(SoundSO soundSO, string soundListName, AudioSource source = null, float volume = 1)
    {
        //Find proper soundlist
        SoundList soundList = FindSoundListByName(soundSO, soundListName);

        AudioClip[] clips = soundList.sounds;

        //Get random clip from soundlist
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];

        if (source)
        {
            source.outputAudioMixerGroup = soundList.mixer;
            source.clip = randomClip;
            source.volume = volume * soundList.volume;
            source.Play();
        }
        else
        {
            instance._audioSource.outputAudioMixerGroup = soundList.mixer;
            instance._audioSource.PlayOneShot(randomClip, volume * soundList.volume);
        }
    }

    private static SoundList FindSoundListByName(SoundSO soundSO, string soundListName)
    {
        foreach (var soundList in soundSO.sounds)
        {
            if (soundList.name == soundListName)
            {
                return soundList;
            }
        }

        //Empty SoundList
        return new SoundList();
    }
}
