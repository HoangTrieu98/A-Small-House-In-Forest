using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorHP : MonoBehaviour
{
    public bool isDead;
    [Header("Components")]
    [SerializeField] private Bar healthBar;
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private Animator animator;

    [Header("Attributes")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private float barFillAmount;
     
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        currentHealth = maxHealth;
    }

    private void ShowDamageText(int damage)
    {
        var damageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        damageText.GetComponent<TextMesh>().text = damage.ToString();
    }

    private void UpdateHealthBar()
    {
        barFillAmount = (float)currentHealth / maxHealth;
        healthBar.SetAmount(barFillAmount);
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth < 0)  return;
        currentHealth -= damage;
        UpdateHealthBar();
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        animator.SetTrigger("isHurt");
        if (damageTextPrefab != null && currentHealth > 0)
        {
            ShowDamageText(damage);
        }
        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("isDead");
    }

    public bool isAlive()
    {
        return currentHealth > 0;
    }

    public void Healing(int healAmount)
    {
        currentHealth += healAmount;
        UpdateHealthBar();
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public int generateAmount(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

}
