using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

	public Transform EnemyPrefab;
	public Transform SpownPoint;

	public float TimeBetvenWaves = 5.0f;
	public Text CountDownText;

	private float CountDown = 2.0f;
	private int WavesNumber = 0;

	void Update() {
		if (CountDown <= 0.0f)
		{
			StartCoroutine(SpownWaves());
			CountDown = TimeBetvenWaves + WavesNumber / 2;
		}

		CountDown -= Time.deltaTime;

		CountDownText.text = Mathf.Floor(CountDown).ToString();
		return;
	}
	IEnumerator SpownWaves() {
		++WavesNumber;
		for (int i = 1; i <= WavesNumber; ++i)
		{
			SpownEnemy();
			yield return new WaitForSeconds(0.4f);
		}
	}

	void SpownEnemy()
	{
		Instantiate(EnemyPrefab, SpownPoint.position, SpownPoint.rotation);
		return;
	}
}
