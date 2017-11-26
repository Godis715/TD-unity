using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPreFub;

    public Transform spawnPoint;
    public float timeBetweenWaves = 2f;
    private float countdown = 2f;

    public Text WaveCountdownText;
    public Vector3 dir = new Vector3(0f, -1f, 0f);
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
        timeBetweenWaves += 0.5f;
        for (int i = 0; i < waveIndex; i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

    }

    void spawnEnemy()
    {
        Instantiate(enemyPreFub, spawnPoint.position+dir, spawnPoint.rotation);
    }
}
