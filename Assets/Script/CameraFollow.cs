using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float Speed, minX, maxX;
    [SerializeField] private Vector3 offset;
    private Vector3 cameraPosition;

    //Move From maxX to minX
    [SerializeField] private float moveSpeed;
    [SerializeField] private float delayTime;
    [SerializeField] private bool isMoveToPlayer;

    private void Start()
    {
        isMoveToPlayer = false;
        StartCoroutine(_CameraMove());
    }
    IEnumerator _CameraMove()
    {
        Vector3 minXPosition = new Vector3(minX, transform.position.y, transform.position.z);
        Vector3 maxXPosition = new Vector3(maxX, transform.position.y, transform.position.z);
        
        while (transform.position != maxXPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, maxXPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(delayTime); 

        while (transform.position != minXPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, minXPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(delayTime);
        isMoveToPlayer = true;
    }

    void Update()
    {
        if (target != null && isMoveToPlayer)
        {
            cameraPosition = target.position + offset;
            cameraPosition.x = Mathf.Clamp(cameraPosition.x, minX, maxX);
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, cameraPosition.x, Speed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }
}
