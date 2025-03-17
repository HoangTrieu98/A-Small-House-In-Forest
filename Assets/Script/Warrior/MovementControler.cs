using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum movingState
{
    isStanding, isWalking, isRunning
}
public class MovementControler : MonoBehaviour
{
    [Header("WarriorHP")]
    [SerializeField] private WarriorHP warriorHP;
    [Header("State")]
    [SerializeField] private AttackControler attackControler;
    [SerializeField] private AudioManager audioManager;
    public movingState currentState;
    private bool isFacingRight;
    private bool isLanded;
    private bool isRegening;

    [Header("Components")]
    [SerializeField] private Bar stamina;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckLength;
    [SerializeField] private LayerMask groundLayer;

    [Header("Attributes")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxStamina;
    private float currentStamina;
    private float staminaDrainRate = 10f;
    private float staminaRegenRate = 10f;
    private Vector2 velocity;
    private float barFillAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentState = movingState.isStanding;
        currentStamina = maxStamina;
        isFacingRight = true;
        isLanded = true;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Warrior"), LayerMask.NameToLayer("Enemy"));
    }

    // Update is called once per frame
    void Update()
    {
        if (warriorHP != null && warriorHP.isAlive())
        {
            if (attackControler.isAttacking == false)
            {
                GroundCheck();
                Jumping();
                Moving();
                Regening();
            }
            if (currentState == movingState.isWalking && attackControler.isAttacking == true)
            {
                rigidBody.velocity = Vector2.zero;
                currentState = movingState.isStanding;
            }
        }
    }
    private void GroundCheck()
    {
        RaycastHit2D raycast = Physics2D.Raycast(groundCheck.position, Vector3.down, groundCheckLength, groundLayer);
        if (raycast.collider != null)
        {
            isLanded = true;
        }
        else
        {
            isLanded = false;
        }
        animator.SetBool("isLanded", isLanded);
    }

    private void Jumping()
    {
      if (Input.GetKeyDown(KeyCode.Space) && isLanded)
        {
            velocity.y = jumpForce;
            rigidBody.velocity = velocity;
            audioManager.PlaySound(WarriorSoundType.Jump);
            animator.SetTrigger("Jump");
        }
    }

    private void Moving()
    {
        float xHorizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.RightShift) && currentStamina > 0)
        {
            xHorizontal *= runSpeed;
            audioManager.PlaySound(WarriorSoundType.Sprint);
            currentState = movingState.isRunning;
            animator.SetBool("isRunning", true);
            currentStamina -= staminaDrainRate * Time.deltaTime;
            UpdateStaminaBar();
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            xHorizontal *= walkSpeed;
            audioManager.StopSound(WarriorSoundType.Sprint);
            audioManager.PlaySound(WarriorSoundType.Walk);
            currentState = movingState.isWalking;
            animator.SetBool("isRunning", false);
        }

        velocity = new Vector2(xHorizontal, rigidBody.velocity.y);
        rigidBody.velocity = velocity;
        animator.SetFloat("Velocity", Mathf.Abs(velocity.x));
        FlipHandle(velocity);
        if (Mathf.Abs(velocity.x) == 0 && currentStamina > 0)
        {
            currentState = movingState.isStanding;
        }
    }

    private void FlipHandle(Vector2 velocity)
    {
        Vector2 localScale = transform.localScale;
        if (isFacingRight)
        {
            if (velocity.x < 0)
            {
                isFacingRight = false;
                localScale.x = -1;
            }
        }
        else
        {
            if (velocity.x > 0)
            {
                isFacingRight = true;
                localScale.x = 1;
            }
        }
        transform.localScale = localScale;
    }

    private void Regening()
    {
        if (currentState != movingState.isRunning && !isRegening)
        {
            StartCoroutine(_RegenStamina());
        }
    }
    IEnumerator _RegenStamina()
    {
        isRegening = true;
        yield return new WaitForSeconds(2f);
        while (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            UpdateStaminaBar();
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
            yield return null;
        }
        isRegening = false;
    }
    private void UpdateStaminaBar()
    {
        barFillAmount = currentStamina / maxStamina;
        stamina.SetAmount(barFillAmount);
    }
}
