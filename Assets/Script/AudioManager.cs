using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WarriorSoundType
{
    Walk, Sprint, Jump, Hurt, Die
}


public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;


    public void PlaySound(WarriorSoundType soundType)
    {
        if (audioSources[(int)soundType] != null)
        {
            audioSources[(int)soundType].Play();
        }
    }

    public void StopSound(WarriorSoundType soundType)
    {
        if (audioSources[(int)soundType] != null)
        {
            audioSources[(int)soundType].Stop();
        }
    }
}
