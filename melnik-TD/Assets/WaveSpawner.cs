using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPreFub;

    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countdown = 5f;

    public Text WaveCountdownText;

    private int waveIndex = 0;

    private void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(spawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        WaveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator spawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void spawnEnemy()
    {
        Instantiate(enemyPreFub, spawnPoint.position, spawnPoint.rotation);
    }
}
