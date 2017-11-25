using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public float range = 15f;
	public Transform target;

	public string enemyTag = "Enemy";

	public Transform center;

	public GameObject nearestEnemy = null;

	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

		float shortestDistance = Mathf.Infinity;
		nearestEnemy = null;

		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			if (distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
		}
		else {
			target = null;
		}
	}
	
	void Update () {
		if (target == null) {
			return;
		}

		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(center.rotation, lookRotation, Time.deltaTime * 8f).eulerAngles;
		center.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (nearestEnemy != null) {
			nearestEnemy.GetComponent<Enemy>().health -= 30f * Time.deltaTime;
			if (nearestEnemy.GetComponent<Enemy>().health <= 0f)
			{
				Destroy(nearestEnemy);
			}
			else {
				nearestEnemy.GetComponent<MeshRenderer>().material.color = Color.Lerp(nearestEnemy.GetComponent<MeshRenderer>().material.color,
					Color.red, nearestEnemy.GetComponent<Enemy>().health / 100f * Time.deltaTime);
			}
		}

	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
