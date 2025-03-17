using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType 
{Slime, Orc, Minotaur }

public enum EnemySoundState
{
    Run, Hurt, Die 
}

public class EnemySoundKey
{
    public EnemyType enemyTyoe;
    public EnemySoundState enemySoundState;
}

public class EnemyAudioManager : MonoBehaviour
{
    [System.Serializable]
    public class soundEntry
    {
        public AudioSource audioSource;
        public EnemySoundKey key;
    }

    public List<soundEntry> soundEntries = new List<soundEntry>();
    public Dictionary<EnemySoundKey, AudioSource> soundDictionary = new Dictionary<EnemySoundKey, AudioSource>();

    private void Awake()
    {
        foreach(var entry in soundEntries)
        {
            soundDictionary[entry.key] = entry.audioSource;
        }
    }

    public void PlaySound(EnemyType enemyType, EnemySoundState soundState)
    {
        EnemySoundKey key = new EnemySoundKey();
        key.enemyTyoe = enemyType;
        key.enemySoundState = soundState;

        if (soundDictionary.TryGetValue(key, out AudioSource audioSource))
        {
            audioSource.Play();
        }
    }
}
