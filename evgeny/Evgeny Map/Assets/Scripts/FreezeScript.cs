using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeScript : MonoBehaviour
{
	public float Range = 10.0f;
	public string EnemyTag = "Enemy";
	// скорострельность пушки 4/сек.
	// сейчас равна 0, так как пушка по идее сразу готова стрелять.
	private float RateOfFire = 4.0f;
	private float TimeOfFreeze = 2.0f;
	GameObject FirstEnemy;
	void Start()
	{
		InvokeRepeating("UpdateTarget", 0.0f, RateOfFire);
	}
	void UpdateTarget()
	{
		GameObject[] Enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
		int SizeArray = Enemies.Length;
		for (int i = 0; i < SizeArray; ++i)
		{
			float DistanceToEnemy = Vector3.Distance(transform.position, Enemies[i].transform.position);
			if (Range >= DistanceToEnemy)
			{
				Enemies[i].GetComponent<Enemy>().TimeOfFreez = TimeOfFreeze;
			}

		}
	}
	void Update()
	{
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, Range);
	}
}
