using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthState : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    public bool isHurt;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        isHurt = false;
        isDead = false;
    }

    public void StartHurt()
    {
        isHurt = true;
        audioManager.PlaySound(WarriorSoundType.Hurt);
    }

    public void EndHurt()
    {
       isHurt = false;
    }

    private void OnDead()
    {
        isDead = true;
        audioManager.PlaySound(WarriorSoundType.Die);
    }
}
