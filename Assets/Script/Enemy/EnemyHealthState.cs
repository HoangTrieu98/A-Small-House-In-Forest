using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthState : MonoBehaviour
{
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
        }

        private void EndHurt()
        {
            isHurt = false;
        }

        private void OnDead()
        {
            isDead = true;
        }
    }
