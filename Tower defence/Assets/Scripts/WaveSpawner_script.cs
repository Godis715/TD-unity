using UnityEngine;
using System.Collections;

public class WaveSpawner_script : MonoBehaviour
{
    [Header("Spawning")]
    public float timeBetweenWaves = 5.0f;
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    private float countdownToWave = 5.0f;
    private int waveIndex = 0;

    void Update()
    {
        countdownToWave -= Time.deltaTime;
        if(countdownToWave <= 0f)
        {
            waveIndex++;
            StartCoroutine(SpawnWave());
            countdownToWave = timeBetweenWaves;
        }
    }

    IEnumerator SpawnWave()
    {
        for (int i = 1; i <= waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
}
