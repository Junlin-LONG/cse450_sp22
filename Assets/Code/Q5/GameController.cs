using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    // State Tracking
    public float timeElapsed;
    public float asteroidDelay;
    public int score;
    public int money;
    public float missileSpeed = 2f;
    public float bonusMultiplier = 1f;

    // Outlets
    public Transform[] spawnPoints;
    public GameObject[] asteroidPrefabs;
    public GameObject explosionPrefab;
    public TMP_Text textScore;
    public TMP_Text textMoney;
    public TMP_Text missleSpeedUpgradeText;
    public TMP_Text bonusUpgradeText;

    // Configuration
    public float minAsteroidDelay = 0.2f;
    public float maxAsteroidDelay = 2f;

    // Methods
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine("AsteroidSpawnTimer");

        score = 0;
        money = 0;
    }

    private void Update()
    {
        // Increment time for game
        timeElapsed += Time.deltaTime;

        // Compute Asteroid Delay
        float decreaseDelayOverTime = maxAsteroidDelay - ((maxAsteroidDelay - minAsteroidDelay) / 30f * timeElapsed);
        asteroidDelay = Mathf.Clamp(decreaseDelayOverTime, minAsteroidDelay, maxAsteroidDelay);

        UpdateDisplay();
    }

    void SpawnAsteroid()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject randomAsteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Spawn
        Instantiate(randomAsteroidPrefab, randomSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator AsteroidSpawnTimer()
    {
        // Wait
        yield return new WaitForSeconds(asteroidDelay);

        // Spawn
        SpawnAsteroid();

        // Repeat
        StartCoroutine("AsteroidSpawnTimer");
    }

    public void EarnPoints(int pointAmount)
    {
        score += Mathf.RoundToInt(pointAmount * bonusMultiplier);
        money += Mathf.RoundToInt(pointAmount * bonusMultiplier);
    }

    void UpdateDisplay()
    {
        textScore.text = score.ToString();
        textMoney.text = money.ToString();
    }

    public void UpgradeMissileSpeed()
    {
        int cost = Mathf.RoundToInt(25 * missileSpeed);

        if(cost <= money)
        {
            money -= cost;

            missileSpeed += 1f;

            missleSpeedUpgradeText.text = "Missile Speed $" + Mathf.RoundToInt(25 * missileSpeed);
        }
    }

    public void UpgradeBonus()
    {
        int cost = Mathf.RoundToInt(100 * bonusMultiplier);

        if(cost <= money)
        {
            money -= cost;

            bonusMultiplier += 1f;

            bonusUpgradeText.text = "Multiplier $" + Mathf.RoundToInt(100 * bonusMultiplier).ToString();
        }
    }
}
