using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float speed = 15f;
	private Transform target;
	private int waypointIndex = 0;
	//Звук смерти
	public AudioClip death;
	//Звук добавления денег
	public AudioClip bonus;
	//Звук упущенного Enemy
	public AudioClip signal;
	//Тег для поиска пилюль
	public string TagSpeedUp = "SpeedUp";
	//Состояние здоровья
	public float health = 100f;
	public GameObject bloodEffect;
	void Start()
	{
		target = Waypoits.points[0];
	}
	void Update()
	{
		if (health <= 0.0f)
		{
			Destroy(gameObject);
			Quaternion rotationBlood = Quaternion.Euler(-90f, 0f, 0f);
			GameObject blood = Instantiate(bloodEffect, transform.position, rotationBlood);
			Destroy(blood, 2f);
			AudioSource.PlayClipAtPoint(bonus, transform.position);
			AudioSource.PlayClipAtPoint(death, transform.position);
			Player.NewMoney += 2f;
			Player.frags++;
			return;
		}
		//Проверка на таргет пилюли
		GameObject[] SpeedUp = GameObject.FindGameObjectsWithTag(TagSpeedUp);
		foreach (GameObject speedUp in SpeedUp)
		{
			if (Vector3.Distance(transform.position, speedUp.transform.position) <= 0.5f)
			{
				StartCoroutine(SpeedCreate());
			}
		}

		//Перемещение к поинту
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
		if (Vector3.Distance(target.position, transform.position) <= 0.4f)
		{
			GetNextWaypoint();
		}
	}

	//Действие пилюли
	IEnumerator SpeedCreate()
	{
		speed *= 2;
		yield return new WaitForSeconds(0.2f);
		speed /= 2;
	}

	//Следующий поинт
	void GetNextWaypoint()
	{
		//проверка на END
		if (waypointIndex >= Waypoits.points.Length - 1)
		{
			//Подсчёт упущенных Enemy
			Player.WinEnemy += 1f;
			//Извещение об упущенном Enemy
			AudioSource.PlayClipAtPoint(signal, transform.position);
			Destroy(gameObject);
			return;
		}
		waypointIndex++;
		target = Waypoits.points[waypointIndex];
	}
}
