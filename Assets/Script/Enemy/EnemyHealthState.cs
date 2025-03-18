using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthState : MonoBehaviour
{
        public AudioSource audioSource;
        public bool isHurt;
        public bool isDead;


        // Start is called before the first frame update
        void Start()
        {
            isHurt = false;
            isDead = false;
        }

        private void StartHurt()
        {
            isHurt = true;
            audioSource.Play();

        }

        private void EndHurt()
        {
            isHurt = false;
        }

        private void OnDead()
        {
            isDead = true;
            audioSource.Play();
        }
    }
