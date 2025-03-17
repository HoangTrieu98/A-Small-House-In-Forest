using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 randonPos = new Vector3(0.5f, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(
            Random.Range(-randonPos.x, randonPos.x), 
            Random.Range(-randonPos.y, randonPos.y), 
            Random.Range(-randonPos.z, randonPos.z));
    }
}
