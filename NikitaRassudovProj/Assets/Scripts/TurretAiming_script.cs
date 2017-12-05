﻿using UnityEngine;

public class TurretAiming_script : MonoBehaviour
{
	private GameObject target;

	[Header("Turret parameters")]
	public float range = 15.0f;
	public float rotationSpeed = 12.5f;

	[Header("Other")]
	public string enemyTag;
	public GameObject rotateDirector;
	public float enemySearchFrequency = 10.0f;

	void Start()
	{
		InvokeRepeating("CheckTarget", 0f, 1.0f / enemySearchFrequency);
	}

	void Update()
	{
		if(target == null)
		{
			return;
		}

		Vector3 arrowToEnemy = target.transform.position - this.transform.position;
		Quaternion toRotate = Quaternion.LookRotation(arrowToEnemy);
		Vector3 rotation = Quaternion.Lerp(rotateDirector.transform.rotation, toRotate, rotationSpeed * Time.deltaTime).eulerAngles;
		rotateDirector.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void CheckTarget()
	{
		if(target == null)
		{
			UpdateTarget();
		}
		else if(Vector3.Distance(this.transform.position, target.transform.position) > range)
		{
			UpdateTarget();
		}
	}

	void UpdateTarget()
	{
		GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(enemyTag);
		GameObject nearestEnemy = null;
		float minimalDistacne = Mathf.Infinity;
		foreach(GameObject currentEnemy in allEnemies)
		{
			Vector3 curDist = this.transform.position - currentEnemy.transform.position;
			if (curDist.magnitude < minimalDistacne)
			{
				minimalDistacne = curDist.magnitude;
				nearestEnemy = currentEnemy;
			}
		}
		if(nearestEnemy != null && minimalDistacne <= range)
		{
			target = nearestEnemy;
		}
		else
		{
			target = null;
		}
		this.GetComponent<TurretShooting_script>().SetTarget(target);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, range);
	}
}
