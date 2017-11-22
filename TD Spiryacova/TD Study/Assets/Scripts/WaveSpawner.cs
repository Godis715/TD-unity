using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

	[Header("Spawner")]
	public Transform enemyPrefab;
	public Transform spawnPoint;

	public float timeBetweenWaves = 5.5f;
	private float countdown = 2f;

	public Text waveCountdown;
	private int waveIndex = 0;

	void Update()
	{
		if (countdown <= 0)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
		waveCountdown.text = Mathf.Round(countdown).ToString();
	}

	IEnumerator SpawnWave()
	{
		++waveIndex;
		
		for (int i = 0; i < waveIndex; ++i)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(0.4f);
		}
		
	}

	void SpawnEnemy()
	{
		Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
	}

}
