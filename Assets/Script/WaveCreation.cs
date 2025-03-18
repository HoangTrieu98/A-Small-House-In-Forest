using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WaveSetting
{
    public string waveName;
    public GameObject[] enemiesType;
    public int numberOfEnemies;
    public float timeToSpawn;
    public float coolDownTimer;
} 

public enum SpawnSide
{
    right, left
}
public class WaveCreation : MonoBehaviour
{
    [Header("Componenets")]
    [SerializeField] WarriorHP warriorHP;
    [SerializeField] AttackControler attackControler;
    [SerializeField] private GameObject instructionPanel;
    [SerializeField] private bool isShowed;
    public SpawnSide currentSpawnSide;

    [Header("Enemy Spotter")]
    [SerializeField] private Image[] enemySpotters;


    [Header("Setting")]
    [SerializeField] private WaveSetting[] Waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Animator animator;

    [Header("CurrentWave Management")]
    [SerializeField] private GameObject levelUpOption;
    [SerializeField] private GameObject finishWavePanel;
    [SerializeField] private GameObject youDiePanel;
    [SerializeField] private Text waveName;
    [SerializeField] private WaveSetting currentWave;
    [SerializeField] private int currentWaveNumber;
    [SerializeField] private int spawnedEnemies;
    [SerializeField] private int eliminatedEnemies;
    [SerializeField] private float delayTime;


    private void Start()
    {
        isShowed = false;
        if (Waves.Length > 0)
        {
            currentWave = Waves[currentWaveNumber];
        }
    }
    void Update()
    {
        delayTime -= Time.deltaTime; 
        if (delayTime < 0) { delayTime = 0; }
        if (currentWave != null && delayTime <= 0)
        {
            if (!isShowed)
            {
                Time.timeScale = 0;
                instructionPanel.SetActive(true);
                isShowed = true;
            }
            else
            {
                SpawnWave();
                if (eliminatedEnemies == currentWave.numberOfEnemies)
                {
                    eliminatedEnemies = 0;
                    if (warriorHP.isAlive())
                    {
                        StartCoroutine(_TimeToChoose());
                    }
                }
            }
        }

        if (warriorHP.isDead == true)
        {
            youDiePanel.SetActive(true);
        }
    }

    IEnumerator _TimeToChoose()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("WaveCompleted", true);
        yield return new WaitForSeconds(1f);
        levelUpOption.SetActive(true);
    }
    private void StartNextWave()
    {
        currentWaveNumber++;
        if (currentWaveNumber < Waves.Length)
        {
            currentWave = Waves[currentWaveNumber];
            currentWave.coolDownTimer = currentWave.timeToSpawn;
            spawnedEnemies = 0;
            eliminatedEnemies = 0;

            waveName.text = Waves[currentWaveNumber].waveName;
            animator.ResetTrigger("3,2,1,Fight");
            animator.SetBool("WaveCompleted", false);
            animator.SetTrigger("3,2,1,Fight");

        }
        else
        {
            finishWavePanel.SetActive(true);
        }
    }

    private void SpawnWave()
    {
        currentWave.coolDownTimer -= Time.deltaTime;
        if (currentWave.coolDownTimer <= 0 && spawnedEnemies < currentWave.numberOfEnemies)
        {
            currentWave.coolDownTimer = currentWave.timeToSpawn;
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            if (Random.value < 0.5f)
            {
                currentSpawnSide = SpawnSide.left;
            }
            else { currentSpawnSide = SpawnSide.right; }
            GameObject randomEnemy = currentWave.enemiesType[Random.Range(0, currentWave.enemiesType.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            EnemyMovement enemy = randomEnemy.GetComponent<EnemyMovement>();
            enemy.spawnSide = currentSpawnSide;
            ShowSPotter(enemy);
            spawnedEnemies++;
        }
    }
    public void EnemiesDied()
    {
        eliminatedEnemies++;
    }


    public void IncreaseDamage()
    {
        levelUpOption.SetActive(false);
        int damageAmount = attackControler.GenerateAmount(10, 20);
        attackControler.increaseDamage(damageAmount);
        StartCoroutine(_DelayStartNextWave());
    }

    public void RestoreHealth()
    {
        levelUpOption.SetActive(false);
        int healAmount = warriorHP.generateAmount(10, 30);
        warriorHP.Healing(healAmount);
        StartCoroutine(_DelayStartNextWave());

    }

    IEnumerator _DelayStartNextWave()
    {
        yield return new WaitForSeconds(1f);
        StartNextWave();
    }

    public void ShowSPotter(EnemyMovement enemy)
    {
        foreach (var spotter in enemySpotters)
        {
            if (spotter != null)
            {
                spotter.gameObject.SetActive(false);
            }
        }

        if (enemy.spawnSide == SpawnSide.left)
        {
            if (enemySpotters[0] != null)
            {
                enemySpotters[0].gameObject.SetActive(true);
            }
        }

        else if (enemy.spawnSide == SpawnSide.right)
        {
            if (enemySpotters[1] != null)
            {
                enemySpotters[1].gameObject.SetActive(true);
            }
        }
        StartCoroutine(_HideEnemySpotters(3f, enemy)); 
    }

    IEnumerator _HideEnemySpotters(float delay, EnemyMovement enemy)
    {
        yield return new WaitForSeconds(delay);

        if (enemy.spawnSide == SpawnSide.left)
        {
            if (enemySpotters[0] != null)
            {
                enemySpotters[0].gameObject.SetActive(false);
            }
        }

        else if (enemy.spawnSide == SpawnSide.right)
        {
            if (enemySpotters[1] != null)
            {
                enemySpotters[1].gameObject.SetActive(false);
            }
        }

    }
}

