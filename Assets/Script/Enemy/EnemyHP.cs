using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [Header("Componenets")]
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private WaveCreation waveManagement;
    [Header("Attributes")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        waveManagement = FindObjectOfType<WaveCreation>();
    }

    private void ShowDamageText(int damage)
    {
        var damageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
        damageText.GetComponent<TextMesh>().text = damage.ToString();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        animator.SetTrigger("isHurt");
        if (damageTextPrefab != null && currentHealth > 0)
        {
            ShowDamageText(damage);
        }
        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    private void Die()
    {
        waveManagement.EnemiesDied();
        animator.SetTrigger("isDead");
        StartCoroutine(_SelfDestroy());
    }
    IEnumerator _SelfDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public bool isAlive()
    {
        return currentHealth > 0;
    }
}
