using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

	public Transform enemyPrefab;

	public Transform spawnPoint;

	public float timeBetweenWaves = 3f;
	private float countdown = 2f;

	private int waveNumber = 0;

	public Text waveCountDown;

	void Update() {
		if (countdown <= 0f) {
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}

		countdown -= Time.deltaTime;

		waveCountDown.text = Mathf.Round(countdown).ToString();

	}

	IEnumerator SpawnWave() {

		Debug.Log(waveNumber.ToString() + " wave yeaaah!!!");

		waveNumber++;

		for (int i = 0; i < waveNumber; i++)
		{
			Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(0.5f);
		}
	}
}
