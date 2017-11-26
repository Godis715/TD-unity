using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public Transform target;
	public float range = 10f;
	public string tagEnemy = "Enemy";
	public Transform PartToRotate;
	public float turnSpeed = 10f;
	//эффект стрельбы
	public GameObject effect;
	//Дамаг
	public float damage = 10f;
	private GameObject nearestEnemy = null;
	//Перезарядка
	private float timeShot = 0.0f;
	//Звук стрельбы
	public AudioClip shot;

	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.1f);
	}
	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagEnemy);
		float shortingDistance = Mathf.Infinity;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortingDistance)
			{
				shortingDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null && shortingDistance <= range)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
	}
	// Update is called once per frame
	void Update () {
		if (target == null)
			return;
		Vector3 dir = target.position - transform.position;
		Quaternion lookRatation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRatation, Time.deltaTime * turnSpeed).eulerAngles;
		PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
		Vector3 positionEffect = new Vector3(PartToRotate.GetChild(0).position.x + 0.15f, PartToRotate.GetChild(0).position.y - 0.12f, PartToRotate.GetChild(0).position.z - 0.5f);
		Instantiate(effect, positionEffect, PartToRotate.rotation);

		//Проверка, прошла ли перезарядка
		if (timeShot <= 0.0f)
		{
			//Стрельба
			Shooting();
			//Обновление времени перезярядка
			timeShot = 0.2f;
		}
		timeShot -= Time.deltaTime;
	}
	void Shooting()
	{
		//Озвучка стрельбы
		AudioSource.PlayClipAtPoint(shot, transform.position);
		//Нанесение дамага
		nearestEnemy.transform.GetComponent<Enemy>().health -= damage;
		//Окрас Enemy в зависимости от суммарного нанесённого урона
		nearestEnemy.transform.GetComponent<Renderer>().material.color = Color.Lerp(nearestEnemy.transform.GetComponent<Renderer>().material.color, Color.red, 0.2f);
	}
	void InDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
