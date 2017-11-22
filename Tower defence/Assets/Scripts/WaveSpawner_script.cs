using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner_script : MonoBehaviour
{
    [Header("Spawning")]
    public float timeBetweenWaves = 5.5f;
    public GameObject enemyPrefab;
    public Transform spawnPoint;

	[Header("Displaying")]
	public Text countDownTimerDisplay;

    private float countdownToWave = 5.0f;
    private int waveIndex = 0;

    void Update()
    {
        countdownToWave -= Time.deltaTime;
		countDownTimerDisplay.text = Mathf.Round(countdownToWave).ToString();

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
