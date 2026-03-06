using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public SoundData[] AudioClips;

    private static bool isApplicationQuit = false;
    private void Awake()
    {
        if (Instance != null && Instance != this && !isApplicationQuit)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(AudioSource audioSource, SoundID id)
    {
        AudioClip[] clips=null;
        foreach (SoundData clip in AudioClips)
        {
            if (clip.Id == id)
            {
                clips=clip.Clips;   
                break;
            }
        }
        audioSource.clip = CasualSoundSelect(clips);
        audioSource.Play();
    }
    private AudioClip CasualSoundSelect(AudioClip[] audioClips)
    {
        int index = Random.Range(0, audioClips.Length);
        return audioClips[index];
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    protected virtual void OnApplicationQuit()
    {
        isApplicationQuit = true;
    }


}
