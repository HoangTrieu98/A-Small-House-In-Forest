using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAtHomePage : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private float idleTime;
    private float walkSpeed = 1;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private Vector2 localScale;
    Vector3 xMoving;


    // Update is called once per frame
    void Update()
    {
        localScale = transform.localScale;
        xMoving = transform.position;
        xMoving += new Vector3(walkSpeed * Time.deltaTime, 0, 0);
        transform.position = xMoving;
        animator.SetBool("isWalking", true);


        if (xMoving.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            animator.SetBool("isWalking", false);
            StartCoroutine(_RelaxingAtMaxX());
        }
        if (xMoving.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            animator.SetBool("isWalking", false);
            StartCoroutine(_RelaxAtMinX());
        }
    }

    IEnumerator _RelaxAtMinX()
    {
        yield return new WaitForSeconds(idleTime);
        localScale.x = 1;
        transform.localScale = localScale;
        walkSpeed = 1;
        animator.SetBool("isWalking", true);
    }
    IEnumerator _RelaxingAtMaxX()
    {
        yield return new WaitForSeconds(idleTime);
        localScale.x = -1;
        transform.localScale = localScale;
        walkSpeed = -1;
        animator.SetBool("isWalking", true);
    }
}
